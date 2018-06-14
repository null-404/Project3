using Project3.Data.Models;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service
{
    public class PageCache : IPageCache
    {
        private readonly ICache cache;
        private readonly IContentManagerService cms;
        public PageCache(ICache cache, IContentManagerService cms)
        {
            this.cache = cache;
            this.cms = cms;
        }
        public async Task<Contents> GetByCid(int cid)
        {
            var pagedata = await GetListAsync();
            return pagedata.Where(m => m.cid == cid).SingleOrDefault();
        }

        public async Task<IList<Contents>> GetListAsync()
        {
            var pagelist = cache.Get("pagelist");
            if (pagelist == null)
            {
                pagelist = await cms.GetPageListAsync();
                cache.Set("pagelist", pagelist);

            }
            return pagelist as IList<Contents>;
        }

        public async Task Refresh()
        {
            cache.Set("pagelist", await cms.GetPageListAsync());
        }
    }
}
