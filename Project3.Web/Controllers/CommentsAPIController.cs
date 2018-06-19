using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using Project3.Web.Models;

namespace Project3.Web.Controllers
{
    public class CommentsAPIController : Controller
    {
        private readonly ICommentManagerService coms;
        public CommentsAPIController(ICommentManagerService coms)
        {
            this.coms = coms;
        }

        [HttpGet]
        public async Task<JsonResult> Get(int cid, int parentcoid, int page = 1, int pagesize = 10, int skip = 0)
        {
            var data = await coms.GetAjaxPageListAsync(cid, page, pagesize, parentcoid, skip);
            var rd = new CommentsModel();
            rd.comments = data;
            rd.totalcontents = data.TotalContents;
            rd.totalpages = data.TotalPages;
            rd.pageindex = data.PageIndex;
            rd.hascount = data.HasCount;
            return Json(rd);
        }
    }
}