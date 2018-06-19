using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Controllers.Attributes
{
    /// <summary>
    /// 自定义错误输出
    /// </summary>
    public class DataErrorAttribute : TypeFilterAttribute
    {
        public DataErrorAttribute() : base(typeof(DataErrorAttributeImpl))
        {

        }

        private class DataErrorAttributeImpl : IActionFilter
        {
            private readonly IOptionsCache optionsCache;
            private readonly IModelMetadataProvider _modelMetadataProvider;
            public DataErrorAttributeImpl(IOptionsCache optionsCache, IModelMetadataProvider _modelMetadataProvider)
            {
                this.optionsCache = optionsCache;
               
                this._modelMetadataProvider = _modelMetadataProvider;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                var controller = context.Controller as BaseController;
                var options = optionsCache.Get().Result;

                if (controller.IsError)
                {

                    var error_result = new ViewResult();
                    error_result.ViewName = "Themes/" + options.theme + "/Error";
                    error_result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                    error_result.ViewData.Add("ErrorTitle", controller.ErrorTitle);
                    error_result.ViewData.Add("ErrorContent", controller.ErrorContent);

                    context.Result = error_result;
                    return;
                }

            }
            //改成抛出异常，然后由异常拦截返回错误页面，否则还要在action另外写判断。（2018/6/19-待完成
            public void OnActionExecuting(ActionExecutingContext context)
            {
                
            }
        }
    }
}
