using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Filters
{
    /// <summary>
    /// 错误拦截
    /// </summary>
    public class ActionErrorFilter : TypeFilterAttribute
    {

        public ActionErrorFilter() : base(typeof(ActionErrorFilterImpl))
        {

        }


        private class ActionErrorFilterImpl : ExceptionFilterAttribute
        {
            private readonly IHostingEnvironment _hostingEnvironment;
            private readonly IModelMetadataProvider _modelMetadataProvider;
            //获得日志的实例  
            //static Logger Logger = LogManager.GetCurrentClassLogger();
            public ActionErrorFilterImpl(
                IHostingEnvironment hostingEnvironment,
                IModelMetadataProvider modelMetadataProvider)
            {
                _hostingEnvironment = hostingEnvironment;
                _modelMetadataProvider = modelMetadataProvider;

            }

            public override void OnException(ExceptionContext context)
            {
                if (_hostingEnvironment.IsDevelopment())
                {
                    // do nothing
                    return;
                }
                var result = new ViewResult { ViewName = "_Error" };
                result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                result.ViewData.Add("Exception", context.Exception.Message);
                // TODO: Pass additional detailed data via ViewData

                //Logger.Error(context.Exception.ToString());
                context.Result = result;

            }
        }
    }
}
