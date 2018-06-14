using Project3.Data;
using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    public class MetasListViewModel
    {
        public PaginatedList<Metas> metas { get; set; }
        public int? pageindex { get; set; }
        public int type { get; set; }
        public Metas meta { get; set; }
    }
}
