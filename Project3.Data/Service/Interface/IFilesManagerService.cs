using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface IFilesManagerService
    {
        Task<IList<Files>> GetFilesByCidAsync(int cid);
        Task<IList<Files>> GetFilesAsync();
        Task<Files> GetFileByFidAsync(int fid);

        Task<Files> AddAsync(Files file);
        Task DeleteByFidAsync(int fid);
        Task DeleteAsync(Files file);
        Task<int> DeleteByFidAsync(int[] fid);
        Task<int> UpdateByCidAsync(int cid, int[] files);

        Task<PaginatedList<Files>> GetListAsync(int? pageindex, int pagesize);

    }
}
