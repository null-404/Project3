using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface ICommentManagerService
    {
        Task<PaginatedList<Comments>> GetPageListAsync(int? pageindex, int pagesize);

        Task<int> DeleteByCoidAsync(int[] coid);

        /// <summary>
        /// 仅读取评论标识ID，通过内容ID
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Task<IList<Comments>> GetListAsync(int cid,int parentcoid=0);

        Task<Comments> GetAsync(int coid);

        Task AddAsync(Comments comment);
    }
}
