using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    /// <summary>
    /// 内容管理服务接口
    /// </summary>
    public interface IContentManagerService
    {
        Task<Contents> AddAsync(Contents _contents);

        Task<Contents> UpdateAsync(Contents _contents);

        Task<PaginatedList<Contents>> GetContentsAsync(int status, string searchstring, int category, int? pageindex, int pagesize, int type);

        /// <summary>
        /// 通过标识ID批量删除内容，返回成功删除条数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Task<int> DeleteByCidAsync(int[] cid);

        /// <summary>
        /// 通过标识ID批量设置内容状态，返回成功操作条数
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Task<int> SetStatusByCidAsync(int[] cid, int status);

        Task<Contents> GetByCidAsync(int cid, bool isadmin = true);

        Task<IList<Metas>> GetMetasAsync(int cid);

        Task UpdateMetasByCidAsync(int cid, string[] name, int type);

        Task<Dictionary<string, IList<Contents>>> GetArchivesAsync();

        Task<Contents> GetPageByCidAsync(int cid);
        Task<IList<Contents>> GetPageListAsync();

        Task<int> CountAsync(int type);

        /// <summary>
        /// 更新阅读数
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task UpdateReadnumAsync(int cid, int add);
    }
}
