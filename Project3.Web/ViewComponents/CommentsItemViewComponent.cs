using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.ViewComponents
{
    public class CommentsItemViewComponent : ViewComponent
    {
        private readonly IOptionsCache optionsCache;
        private readonly ICommentManagerService cms;
        public CommentsItemViewComponent(IOptionsCache optionsCache, ICommentManagerService cms)
        {
            this.optionsCache = optionsCache;
            this.cms = cms;
        }
        //通过内容标识ID和评论父级ID获取评论
        public async Task<IViewComponentResult> InvokeAsync(
                int cid, int parentcoid = 0)
        {
            var options = await optionsCache.Get();

            var data = await cms.GetListAsync(cid, parentcoid);
            return View(options.theme + "/View", data);
        }
    }
}
