using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Service.Interface;
using Project3.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Attributes
{
    /// <summary>
    /// [弃用]消息
    /// </summary>
    public class MessageAttribute : TypeFilterAttribute
    {
        public MessageAttribute() : base(typeof(MessageAttributeAttributeImpl))
        {

        }

        private class MessageAttributeAttributeImpl : IActionFilter
        {
            private readonly IOptionsCache optionsCache;
            private readonly IModelMetadataProvider _modelMetadataProvider;
            public MessageAttributeAttributeImpl(IOptionsCache optionsCache, IModelMetadataProvider _modelMetadataProvider)
            {
                this.optionsCache = optionsCache;
               
                this._modelMetadataProvider = _modelMetadataProvider;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                //var controller = context.Controller as BaseController;
                //var options = optionsCache.Get().Result;

                //if (controller.//Message.IsShow)
                //{

                //    var error_result = new ViewResult();
                //    error_result.ViewName = "Themes/" + options.theme + "/_Message";
                //    error_result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                //    error_result.ViewData.Add("Message.Title", controller.Message.Title);
                //    error_result.ViewData.Add("//Message.Content", controller.//Message.Content);

                //    context.Result = error_result;
                //    context.Canceled = true;
                //    return;
                //}

            }
            //改成抛出异常，然后由异常拦截返回错误页面，否则还要在action另外写判断。（2018/6/19-待完成
            public void OnActionExecuting(ActionExecutingContext context)
            {
                bool a = true;
      
            }
        }
    }
}
