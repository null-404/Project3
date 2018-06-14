using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Extensions;
using Project3.Web.Areas.Admin.Filters;
using Project3.Web.Areas.Admin.Models;
using Project3.Web.Models;

namespace Project3.Web.Areas.Admin.Controllers
{

    public class ContentController : BaseController
    {
        private readonly IContentManagerService cms;
        private readonly IMetasManagerService mms;
        private readonly IFilesManagerService fms;
        private readonly IHostingEnvironment hostingEnvironment;
        public ContentController(IContentManagerService cms, IMetasManagerService mms, IFilesManagerService fms, IHostingEnvironment hostingEnvironment)
        {
            this.cms = cms;
            this.mms = mms;
            this.fms = fms;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region 视图-内容管理列表界面
        public async Task<IActionResult> List(string searchstring, int category, int pageindex = 1, int status = -1, int type = 0)
        {

            var VM = new ContentListViewModel();
            VM.searchstring = searchstring;
            VM.category = category;
            VM.pageindex = pageindex;
            VM.status = status;
            if (type == 0)
            {
                //文章类型才获取分类
                VM.metas = await mms.GetMetasAsync(0);
            }
            VM.type = type;
            VM.contents = await cms.GetContentsAsync(status, searchstring, category, pageindex, 10, type);
            return View(VM);

        }
        #endregion

        #region 操作-删除内容
        [Alert]
        [HttpPost]
        public async Task<IActionResult> Delete_Post(int type, params int[] cid)
        {

            int x = await cms.DeleteByCidAsync(cid);
            if (x > 0)
            {
                Alert.Content = string.Format("成功删除 {0} 条数据", x);
            }
            else
            {
                Alert.Content = "操作数据失败";
                Alert.Level = AlertModel.AlertType.Danger;
            }
            return RedirectToAction(nameof(List), new { type });
        }
        #endregion

        #region 视图-编辑界面
        public async Task<IActionResult> Edit(int cid, int type)
        {
            var VM = new ContentEditViewModel();
            VM.Type = type;
            VM.content = new Project3.Data.Models.Contents();
            VM.selectedmetas = new List<Project3.Data.Models.Metas>();
            if (cid > 0)
            {
                VM.content = await cms.GetByCidAsync(cid);
                VM.selectedmetas = await cms.GetMetasAsync(cid);
                VM.files = await fms.GetFilesByCidAsync(cid);
                VM.Type = VM.content.type;
            }
            else
            {
                VM.files = await fms.GetFilesAsync();
            }
            VM.metas = await mms.GetMetasAsync(-1);

            return View(VM);
        }
        #endregion

        #region 操作-上传附件
        #region 验证是否是图片
        public bool ValidateImages(string ext)
        {
            string[] allows = { ".jpg", ".png", ".gif" };
            if (Array.IndexOf(allows, ext) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {

            var saveDirectory = "files";

            int.TryParse(Request.Form["cid"].ToString(), out int cid);
            var files = new List<Files>();


            foreach (var formFile in Request.Form.Files)
            {
                var n = formFile.FileName;
                string extname = Path.GetExtension(formFile.FileName).ToLower();
                //验证文件
                if (ValidateImages(extname) && formFile.Length > 0)
                {

                    var webpath = Path.Combine(
                        saveDirectory,
                        DateTime.Now.ToString("yyyy"),
                        DateTime.Now.ToString("MM"),
                        DateTime.Now.ToString("dd"),
                        DateTime.Now.ToString("mmss") + extname);

                    var savepath = Path.Combine(hostingEnvironment.WebRootPath,
                        webpath);

                    //创建文件夹
                    if (!Directory.Exists(Path.GetDirectoryName(savepath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(savepath));
                    }
                    using (var stream = new FileStream(savepath, FileMode.Create))
                    {
                        //写入文件
                        await formFile.CopyToAsync(stream);



                        var file = new Files();
                        file.cid = cid;
                        file.createtime = DateTime.Now;
                        file.filename = n;
                        file.filepath = "/" + webpath.Replace("\\", "/");
                        file.size = FileExtension.CountSize(formFile.Length);
                        //写入数据库
                        await fms.AddAsync(file);


                        files.Add(file);
                    }
                }
            }



            return Json(files);
        }
        #endregion

        #region 操作-删除附件
        [HttpGet]
        public async Task<JsonResult> DeleteFile(int fid)
        {
            var result = new ResultModel();
            result.issuccess = false;

            var file = await fms.GetFileByFidAsync(fid);

            if (file != null)
            {
                //删除本地文件
                //if (!string.IsNullOrEmpty(file.filepath))
                //{
                //    var filepath = hostingEnvironment.WebRootPath + file.filepath.Replace("/", "\\");

                //    if (System.IO.File.Exists(filepath))
                //    {
                //        System.IO.File.Delete(filepath);
                //    }
                //}
                //从数据库移除
                await fms.DeleteAsync(file);
                result.issuccess = true;

            }



            return Json(result);


        }
        #endregion

        #region 操作-提交内容
        [Alert]
        [HttpPost]
        public async Task<JsonResult> EditPost(int cid, string title,
            string content, string banner,
            string excerpt, int allowcomment,
            int status, int type,
            int index,
            string[] category, string[] tags,
            int[] files)
        {

            var result = new ResultModel();

            var post = new Contents();
            if (cid > 0)
            {
                post = await cms.GetByCidAsync(cid);
                Alert.Content = "内容已更新";
            }
            else
            {
                post.createtime = DateTime.Now;
                Alert.Content = "内容创建成功";

            }
            post.title = title;
            post.content = content;
            post.banner = banner;
            post.excerpt = excerpt;
            post.index = index;
            post.allowcomment = allowcomment;
            post.status = status;
            post.type = type;
            post.updatetime = DateTime.Now;
            //写入数据库
            await cms.UpdateAsync(post);

            //对文章内容更新分类和标签
            if (type == 0)
            {
                //更新分类
                await cms.UpdateMetasByCidAsync(post.cid, category, 0);
                //更新标签
                await cms.UpdateMetasByCidAsync(post.cid, tags, 1);
            }

            //更新附件
            await fms.UpdateByCidAsync(post.cid, files);
            result.issuccess = true;

            return Json(result);


        }
        #endregion
    }
}