using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Extensions
{
    public class ContentExtension
    {
        private readonly IContentManagerService cms;
        public ContentExtension(IContentManagerService cms)
        {
            this.cms = cms;
        }
     
        public async Task<string> GetTitleByCid(int cid)
        {
            var content = await cms.GetByCidAsync(cid);
            if (content != null)
            {
                return content.title;
            }
            return "";
        }
    }
}
