using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Widget
{
    /// <summary>
    /// Action 消息提示组件
    /// </summary>
    public class MessageWidget : TypeFilterAttribute
    {

        public MessageWidget() : base(typeof(MessageWidgetImpl))
        {
            Order = 2;

        }


        private class MessageWidgetImpl : ExceptionFilterAttribute
        {
            private readonly IHostingEnvironment _hostingEnvironment;
            private readonly IModelMetadataProvider _modelMetadataProvider;
            private readonly IOptionsCache _optionsCache;
            //获得日志的实例  
            //static Logger Logger = LogManager.GetCurrentClassLogger();
            public MessageWidgetImpl(
                IHostingEnvironment hostingEnvironment,
                IModelMetadataProvider modelMetadataProvider,
                IOptionsCache optionsCache)
            {
                _hostingEnvironment = hostingEnvironment;
                _modelMetadataProvider = modelMetadataProvider;
                _optionsCache = optionsCache;
            }

            public override void OnException(ExceptionContext context)
            {
                if(context.Exception is MessageException)
                {
                    var options = _optionsCache.Get().Result;
                    MessageException me = context.Exception as MessageException;
                    var result = new ViewResult { ViewName = "Themes/" + options.theme + "/_Message" };
                    result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);

                    result.ViewData.Add("Message.Title", me.Message.Title);
                    result.ViewData.Add("Message.Content", me.Message.Content);
                    context.Result = result;
                    context.ExceptionHandled = true;
                }
                
                
                

            }
        }
    }
}
