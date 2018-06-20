using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Web.Areas.Admin.Models;
using Project3.Data.Service.Interface;
using Project3.Web.Areas.Admin.Filters;
using Project3.Extensions;

namespace Project3.Web.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentManagerService cms;
        private readonly ContentExtension ce;
        public CommentController(ICommentManagerService cms, ContentExtension ce)
        {
            this.cms = cms;
            this.ce = ce;
        }
     

        #region 视图-内容管理列表界面
        public async Task<IActionResult> List(int pageindex = 1)
        {

            var VM = new CommentListViewModel(ce);
            VM.comments = await cms.GetPageListAsync(pageindex, 10);
            VM.pageindex = pageindex;

            return View(VM);

        }
        #endregion

        #region 操作-删除内容
        [Alert]
        [HttpPost]
        public async Task<IActionResult> Delete_Post(int type, params int[] coid)
        {

            int x = await cms.DeleteByCoidAsync(coid);
            if (x > 0)
            {
                Alert.Content = string.Format("成功删除 {0} 条评论", x);
            }
            else
            {
                Alert.Content = "删除评论失败";
                Alert.Level = AlertModel.AlertType.Danger;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}