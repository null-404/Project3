using Project3.Data;
using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    public class DefaultIndexViewModel
    {
        public int TotalArticles { get; set; }

        public int TotalPages { get; set; }

        public int TotalComments { get; set; }

        public PaginatedList<Comments> Comments { get; set; }
    }
}
