using Project3.Data;
using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    public class ContentListViewModel
    {
        public PaginatedList<Contents> contents { get; set; }

        public IList<Metas> metas { get; set; }


        public int status { get; set; }

        public string searchstring { get; set; }

        public int category { get; set; }

        public int? pageindex { get; set; }

        public int type { get; set; }
    }
}
