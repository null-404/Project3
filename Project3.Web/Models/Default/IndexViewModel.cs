using Project3.Data;
using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Models.Default
{
    public class IndexViewModel : BaseViewModel
    {
        public PaginatedList<Contents> contentlist { get; set; }

        public string search { get; set; }

        public int? mid { get; set; }

        public Metas meta { get; set; }
    }
}
