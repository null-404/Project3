using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Web.Areas.Admin.Filters;
using Project3.Web.Areas.Admin.Models;

namespace Project3.Web.Areas.Admin.Controllers
{
    public class MetasController : BaseController
    {
        private readonly IMetasManagerService mms;

        public MetasController(IMetasManagerService mms)
        {
            this.mms = mms;
        }

     

        #region 视图-内容管理列表界面
        public async Task<IActionResult> List(int mid, int type = 0, int pageindex = 1)
        {

            var VM = new MetasListViewModel();
            VM.type = type;
            VM.metas = await mms.GetListAsync(type, pageindex, 10);
            VM.pageindex = pageindex;
            VM.meta = new Project3.Data.Models.Metas();
            if (mid > 0)
            {
                VM.meta = await mms.GetByMidAsync(mid);
            }
            return View(VM);

        }
        #endregion
        #region 操作-删除内容
        [Alert]
        [HttpPost]
        public async Task<IActionResult> Delete_Post(int type, params int[] mid)
        {

            int x = await mms.DeleteByMidAsync(mid);
            if (x > 0)
            {
                Alert.Content = string.Format("成功删除 {0} 项", x);
            }
            else
            {
                Alert.Content = "删除失败";
                Alert.Level = AlertModel.AlertType.Danger;
            }
            return RedirectToAction(nameof(List), new { type });
        }
        #endregion
        #region 操作-创建/编辑内容
        [Alert]
        [HttpPost]
        public async Task<IActionResult> Edit_Post(int mid, string name, int type)
        {
            var meta = mid == 0 ? new Metas() : await mms.GetByMidAsync(mid);
            meta.name = name;
            meta.type = type;
            if (mid > 0)
            {
                await mms.UpdateAsync(meta);
                Alert.Content = string.Format("已更新");
            }
            else
            {
                await mms.AddAsync(meta);
                Alert.Content = string.Format("已创建");

            }


            return RedirectToAction(nameof(List), new { type });
        }
        #endregion
    }
}