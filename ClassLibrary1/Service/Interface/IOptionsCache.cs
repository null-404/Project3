using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface IOptionsCache
    {
        /// <summary>
        /// 从缓存中获取配置项（当缓存失效/首次获取时从数据库中读取）
        /// </summary>
        /// <returns></returns>
        Task<OptionsModel> Get();

        /// <summary>
        /// 从数据库刷新配置项缓存
        /// </summary>
        /// <returns></returns>
        Task Refresh();
    }
}
