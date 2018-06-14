using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Service.Interface;
using Project3.Web.Areas.Admin.Filters;
using Project3.Web.Areas.Admin.Models;

namespace Project3.Web.Areas.Admin.Controllers
{
    public class OptionsController : BaseController
    {
        private readonly IOptionsManagerService oms;
        private readonly IOptionsCache optionsCache;

        public OptionsController(IOptionsManagerService oms, IOptionsCache optionsCache)
        {
            this.oms = oms;
            this.optionsCache = optionsCache;
        }
        public async Task<IActionResult> Index()
        {
            var VM = new OptionsIndexViewModel();
            VM.options = await oms.GetAsync();
            return View(VM);


        }

        [Alert]
        [HttpPost]
        public async Task<IActionResult> EditPost()
        {
            var data = new Dictionary<string, object>();
            foreach (string name in Request.Form.Keys)
            {
                var value = Request.Form[name];
                data.Add(name, Request.Form[name].ToString());
            }
            int x = await oms.UpdateAsync(data);
            if (x > 0)
            {

                Alert.Content = string.Format("成功更新 {0} 项设置", x);
                //更新缓存
                await optionsCache.Refresh();
            }
            else
            {
                Alert.Content = "更新设置失败";
                Alert.Level = AlertModel.AlertType.Danger;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}