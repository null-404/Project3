using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project3.Data.Service.Interface;
using Project3.Web.Attributes;
using Project3.Web.Filters;
using Project3.Web.Models;
using Project3.Web.Widget;

namespace Project3.Web.Controllers
{
  
    [MessageWidget]
    [ActionErrorFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// Action Message消息提示
        /// </summary>
        public Message Message { get; set; } = new Message();
       
        /// <summary>
        /// 用户IP
        /// </summary>
        public string RemoteIP
        {

            get
            {
                return Request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
    }
}