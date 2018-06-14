using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface ICommentManagerService
    {
        Task<PaginatedList<Comments>> GetListAsync(int? pageindex, int pagesize);

        Task<int> DeleteByCoidAsync(int[] coid);
    }
}
