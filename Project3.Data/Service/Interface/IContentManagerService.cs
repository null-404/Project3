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

        Task<PaginatedList<Contents>> GetContentsAsync(int status,string searchstring,int category,int? pageindex,int pagesize,int type);

        Task<int> DeleteByCidAsync(int[] cid);

        Task<Contents> GetByCidAsync(int cid);

        Task<IList<Metas>> GetMetasAsync(int cid);

        Task UpdateMetasByCidAsync(int cid,string[] name, int type);

        Task<Dictionary<string, IList<Contents>>> GetArchivesAsync();

        Task<Contents> GetPageByCidAsync(int cid);
        Task<IList<Contents>> GetPageListAsync();
    }
}
