using Project3.Data;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    public class CommentListViewModel
    {
        public ContentExtension contentExtension;

        public CommentListViewModel(ContentExtension ce)
        {
            this.contentExtension = ce;
        }
        public PaginatedList<Comments> comments { get; set; }
        public int? pageindex { get; set; }
    }
}
