using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Project3.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Filters
{
    public class AlertAttribute : TypeFilterAttribute
    {
        public AlertAttribute() : base(typeof(ResultAttributeImpl))
        {

        }

        private class ResultAttributeImpl : IActionFilter
        {
          
            public ResultAttributeImpl()
            {
                
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                var controller = context.Controller as BaseController;
                controller.TempData["alert"] = JsonConvert.SerializeObject(controller.Alert);
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {

            }
        }


    }
}
