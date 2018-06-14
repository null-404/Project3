using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project3.Web.Models;
using Project3.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project3.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BaseController : Controller
    {
        public AlertModel Alert { get; set; } = new AlertModel();


    }
}
