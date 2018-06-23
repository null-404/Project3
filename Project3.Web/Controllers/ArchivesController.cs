using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using Project3.Web.Attributes;
using Project3.Web.Models.ArchivesVM;

namespace Project3.Web.Controllers
{
    public class ArchivesController : BaseController
    {
        private readonly IContentManagerService cms;
        public ArchivesController(IContentManagerService cms)
        {
            this.cms = cms;
        }

        [Theme]
        public async Task<IActionResult> Index()
        {

            var VM = new IndexViewModel();
            VM.archives = await cms.GetArchivesAsync();
            ViewData.Model = VM;
            return View();
        }
    }
}