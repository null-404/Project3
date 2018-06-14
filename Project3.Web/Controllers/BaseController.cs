using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project3.Data.Service.Interface;
using Project3.Web.Filters;

namespace Project3.Web.Controllers
{
    [ActionErrorFilter]
    public class BaseController : Controller
    {
        
       
    }
}