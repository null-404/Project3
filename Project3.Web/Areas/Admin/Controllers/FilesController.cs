using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using Project3.Extensions;
using Project3.Web.Areas.Admin.Filters;
using Project3.Web.Areas.Admin.Models;

namespace Project3.Web.Areas.Admin.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IFilesManagerService fms;
        private readonly IContentManagerService cms;
        private readonly ContentExtension ce;

        public FilesController(IFilesManagerService fms, IContentManagerService cms, ContentExtension ce)
        {
            this.fms = fms;
            this.cms = cms;
            this.ce = ce;
        }
        public async Task<IActionResult> List(int pageindex = 1)
        {
            var VM = new FilesListViewModel(ce);
            VM.files = await fms.GetListAsync(pageindex, 10);
            VM.pageindex = pageindex;
           
            return View(VM);
        }
        #region 操作-删除内容
        [Alert]
        [HttpPost]
        public async Task<IActionResult> Delete_Post(params int[] fid)
        {

            int x = await fms.DeleteByFidAsync(fid);
            if (x > 0)
            {
                Alert.Content = string.Format("成功删除 {0} 项", x);
            }
            else
            {
                Alert.Content = "删除失败";
                Alert.Level = AlertModel.AlertType.Danger;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}