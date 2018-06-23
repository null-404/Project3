using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using Project3.Web.Attributes;
using Project3.Web.Models.MetasVM;

namespace Project3.Web.Controllers
{
    public class MetasController : BaseController
    {
        private readonly IMetasManagerService mms;
        public MetasController(IMetasManagerService mms)
        {
            this.mms = mms;
        }

        [Theme]
        public async Task<IActionResult> Index()
        {
            var VM = new IndexViewModel();
            VM.metas = await mms.GetMetasAsync(-1);
            ViewData.Model = VM;
            return View();
        }
    }
}