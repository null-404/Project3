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
        private readonly IContentManagerService cms;
        public PageController(IContentManagerService cms)
        {
            this.cms = cms;

        }

        [Theme]
        public async Task<IActionResult> Index(int cid)
        {
            var VM = new IndexViewModel();
            VM.content = await cms.GetPageByCidAsync(cid);
            ViewData.Model = VM;
            return View();
        }
    }
}