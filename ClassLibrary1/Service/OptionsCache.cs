using Project3.Data.Models;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service
{
    public class OptionsCache : IOptionsCache
    {
        private readonly ICache cache;
        private readonly IOptionsManagerService oms;

        public OptionsCache(ICache cache, IOptionsManagerService oms)
        {
            this.cache = cache;
            this.oms = oms;
        }

        public async Task<OptionsModel> Get()
        {
            var options = cache.Get("options");
            if (options == null)
            {
                options = await oms.GetAsync();
                cache.Set("options", options);
            }
            return options as OptionsModel;
        }

        public async Task Refresh()
        {
            var options = await oms.GetAsync();
            cache.Set("options", options);
        }
    }
}
