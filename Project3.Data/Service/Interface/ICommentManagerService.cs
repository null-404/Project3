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
        Task<IList<Comments>> GetListAsync(int cid, int parentcoid = 0);

        Task<Comments> GetAsync(int coid);

        Task AddAsync(Comments comment);

        /// <summary>
        /// AJAX获取评论
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="parentcoid"></param>
        /// <returns></returns>
        Task<PaginatedList<CommentJsonModel>> GetAjaxPageListAsync(int cid, int? page, int pagesize, int parentcoid,int skip);

        Task<int> CountAsync();
    }
}
