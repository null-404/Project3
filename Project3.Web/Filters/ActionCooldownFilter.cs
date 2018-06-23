using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Enums;
using Project3.Data.Service.Interface;
using Project3.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Filters
{
    /// <summary>
    /// Action 操作冷却拦截器
    /// </summary>
    public class ActionCooldownFilter : TypeFilterAttribute
    {

        public ActionCooldownFilter(ActionCooldownType actionCooldownType) : base(typeof(ActionCooldownFilterImpl))
        {
            Arguments = new object[] { actionCooldownType };
        }

        private class ActionCooldownFilterImpl : IActionFilter
        {
            private readonly IOptionsCache _optionsCache;
            private readonly IModelMetadataProvider _modelMetadataProvider;
            private readonly IActionCooldownService _actionCooldownService;
            private readonly ActionCooldownType _actionCooldownType;
            public ActionCooldownFilterImpl(IOptionsCache _optionsCache,IModelMetadataProvider _modelMetadataProvider, IActionCooldownService _actionCooldownService, ActionCooldownType actionCooldownType)
            {
                this._optionsCache = _optionsCache;
                this._modelMetadataProvider = _modelMetadataProvider;
                this._actionCooldownService = _actionCooldownService;
                this._actionCooldownType = actionCooldownType;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                string remoteIP = context.HttpContext.Connection.RemoteIpAddress.ToString();


               

                if (!_actionCooldownService.IsCoolingCompletion(_actionCooldownType, remoteIP))
                {
                    var options = _optionsCache.Get().Result;

                    string cdtime = "";
                    switch (_actionCooldownType)
                    {
                        case ActionCooldownType.CommentsPost:
                            cdtime = options.postcommentscd;
                            break;
                    }

                    var controller = context.Controller as BaseController;

                    var Message_result = new ViewResult();
                    Message_result.ViewName = "Themes/" + options.theme + "/_Message";
                    Message_result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                    Message_result.ViewData.Add("Message.Title", "你的操作过于频繁");
                    Message_result.ViewData.Add("Message.Content", $"请等待：{cdtime}秒后再尝试此操作。");

                    context.Result = Message_result;
                }


            }
        }
    }
}
