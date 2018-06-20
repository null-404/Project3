using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project3.Data.Service.Interface;
using Project3.Web.Controllers.Attributes;
using Project3.Web.Models;

namespace Project3.Web.Controllers
{
    public class CommentsAPIController : BaseController
    {
        private readonly ICommentManagerService coms;
        private readonly IOptionsCache optionsCache;
        public CommentsAPIController(ICommentManagerService coms, IOptionsCache optionsCache)
        {
            this.coms = coms;
            this.optionsCache = optionsCache;
        }


        [HttpGet]
        public async Task<JsonResult> Get(int cid, int parentcoid, int page = 1, int pagesize = 10, int skip = 0)
        {
            var options = await optionsCache.Get();
            var data = await coms.GetAjaxPageListAsync(cid, page, pagesize, parentcoid, skip);
            var rd = new CommentsModel();
            rd.comments = data;
            if (data != null)
            {
                rd.totalcontents = data.TotalContents;
                rd.totalpages = data.TotalPages;
                rd.pageindex = data.PageIndex;
                rd.hascount = data.HasCount;
            }
            //return Json(rd);
            //return Json(PartialView("Themes/" + options.theme + "/CommentsAPI/Comments", data));

            return Json(rd);
        }

        //[Theme]
        //public async Task<PartialViewResult> Comments(int coid)
        //{
        //    var options = await optionsCache.Get();
        //    var comments = await coms.GetAsync(coid);
        //    return PartialView("Themes/" + options.theme + "/CommentsAPI/Comments", comments);
        //}
    }
}