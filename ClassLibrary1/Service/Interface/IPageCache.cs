using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface IPageCache
    {
        Task<IList<Contents>> GetListAsync();

        Task<Contents> GetByCid(int cid);

        Task Refresh();
    }
}
