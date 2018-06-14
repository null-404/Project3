using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface IMetasManagerService
    {
        /// <summary>
        /// 通过类型获取元素
        /// </summary>
        /// <param name="type">0分类，1标签</param>
        /// <returns></returns>
        Task<IList<Metas>> GetMetasAsync(int type);

        Task<PaginatedList<Metas>> GetListAsync(int type,int? pageindex, int pagesize);
        Task<int> DeleteByMidAsync(int[] coid);

        Task<Metas> GetByMidAsync(int mid);

        Task<Metas> UpdateAsync(Metas meta);

        Task<Metas> AddAsync(Metas meta);

    }
}
