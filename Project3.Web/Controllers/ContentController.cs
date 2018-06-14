using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Web.Controllers.Attributes;
using Project3.Web.Models.Content;

namespace Project3.Web.Controllers
{
    public class ContentController : BaseController
    {

        private readonly IContentManagerService cms;


        public ContentController(IContentManagerService cms)
        {
            this.cms = cms;

        }

        [Theme]
        public async Task<IActionResult> Index(int cid)
        {

            var VM = new IndexViewModel();
            VM.content = await cms.GetByCidAsync(cid, false);
            if (VM.content == null)
            {
                IsError = true;
            }
            VM.metas = await cms.GetMetasAsync(cid);
            ViewData.Model = VM;


            return View();
        }
    }
}