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
    public class FilesListViewModel
    {
        public ContentExtension contentExtension;

        public FilesListViewModel(ContentExtension ce)
        {
            this.contentExtension = ce;
        }


        public PaginatedList<Files> files { get; set; }
        public int? pageindex { get; set; }

        
        
    }
}
