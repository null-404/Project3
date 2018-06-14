using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface IOptionsManagerService
    {
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="data">接受匿名对象</param>
        /// <returns></returns>
        Task<int> UpdateAsync(object data);

        Task<int> UpdateAsync(Dictionary<string,object> data);

        Task<OptionsModel> GetAsync();

        Task AddAsync(object data);

        /// <summary>
        /// 配置项条数
        /// </summary>
        /// <returns></returns>
        Task<int> Count();
    }
}
