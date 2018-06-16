using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Web.Controllers.Attributes;
using Project3.Web.Models.ContentVM;

namespace Project3.Web.Controllers
{
    public class ContentController : BaseController
    {

        private readonly IContentManagerService cms;
        private readonly ICommentManagerService coms;
        private readonly IPageCache pageCache;
        public ContentController(IContentManagerService cms, ICommentManagerService coms, IPageCache pageCache)
        {
            this.cms = cms;
            this.coms = coms;
            this.pageCache = pageCache;
        }
        #region 文章
        [Theme]
        public async Task<IActionResult> Article(int cid)
        {

            var VM = new ArticleViewModel();
            VM.content = await cms.GetByCidAsync(cid, false);
            if (VM.content == null)
            {
                IsError = true;
                ErrorContent = "内容似乎已经丢失，请返回首页。";
            }
            VM.metas = await cms.GetMetasAsync(cid);
            //获取评论
            VM.comments = await coms.GetListAsync(cid);
            ViewData.Model = VM;


            return View();
        }
        #endregion

        #region 页面
        [Theme]
        public async Task<IActionResult> Page(int cid)
        {
            var VM = new ArticleViewModel();
            VM.content = await pageCache.GetByCid(cid);
            if (VM.content == null)
            {
                IsError = true;
                ErrorContent = "页面似乎已经丢失了，请返回首页。";
            }
            ViewData.Model = VM;
            return View();
        }
        #endregion

        
        [HttpPost]
        public async Task<IActionResult> Comment_Post(int cid, int type, int parentcoid, string nickname, string email, string content)
        {
            var contentdata = await cms.GetByCidAsync(cid, false);
            if (contentdata == null)
            {
                IsError = true;
                ErrorContent = "评论的内容似乎已经丢失，请返回。";
              
            }
            var comment = new Comments();
            comment.cid = cid;
            comment.nickname = nickname;
            comment.mail = email;
            comment.content = content;
            comment.createtime = DateTime.Now;
            comment.parentcoid = 0;


            if (parentcoid > 0)
            {
                var parentcomment = await coms.GetAsync(parentcoid);
                if (parentcomment == null)
                {
                    IsError = true;
                    ErrorContent = "回复的评论已被删除，请返回重新提交。";
                }
                else
                {
                    comment.parentcoid = parentcomment.parentcoid==0?parentcoid: parentcomment.parentcoid;
                    comment.reply = parentcomment.nickname;
                }
            }
            if (TryValidateModel(comment))
            {
                await coms.AddAsync(comment);
            }
            else
            {
                IsError = true;
                ErrorContent = "输入有误，请返回重新提交。";
            }
           

            return RedirectToAction(type == 0 ? nameof(Article) : nameof(Page), new { cid });
        }


    }

}