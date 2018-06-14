using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using Project3.Web.Controllers.Attributes;
using Project3.Web.Models.Page;
namespace Project3.Web.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPageCache pageCache;
        public PageController(IPageCache pageCache)
        {
            this.pageCache = pageCache;

        }

        [Theme]
        public async Task<IActionResult> Index(int cid)
        {
            var VM = new IndexViewModel();
            VM.content = await pageCache.GetByCid(cid);
            ViewData.Model = VM;
            return View();
        }
    }
}