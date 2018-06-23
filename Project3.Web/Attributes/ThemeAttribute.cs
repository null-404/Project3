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
    /// 主题特性
    /// </summary>
    public class ThemeAttribute : TypeFilterAttribute
    {
        public ThemeAttribute() : base(typeof(ThemeAttributeImpl))
        {

        }

        private class ThemeAttributeImpl : IActionFilter
        {
            private readonly IOptionsCache optionsCache;
            private readonly IContentManagerService cms;
            private readonly IModelMetadataProvider _modelMetadataProvider;
            private readonly IPageCache pageCache;
            public ThemeAttributeImpl(IOptionsCache optionsCache, IContentManagerService cms, IPageCache pageCache, IModelMetadataProvider _modelMetadataProvider)
            {
                this.optionsCache = optionsCache;
                this.cms = cms;
                this.pageCache = pageCache;
                this._modelMetadataProvider = _modelMetadataProvider;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {

                var controller = context.Controller as BaseController;
                var options = optionsCache.Get().Result;



                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var result = new ViewResult();
                result.ViewName = "Themes/" + options.theme + "/" + controllerActionDescriptor.ControllerName + "/" + controllerActionDescriptor.ActionName;

                result.ViewData = controller.ViewData;


                var pagelist = pageCache.GetListAsync().Result;

                result.ViewData.Add("pagelist", pagelist);
                result.ViewData.Add("options", options);

                context.Result = result;

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {

            }
        }
    }
}
