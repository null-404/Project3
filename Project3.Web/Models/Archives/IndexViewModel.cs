using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Models.Archives
{
    public class IndexViewModel : BaseViewModel
    {
        public Dictionary<string, IList<Contents>> archives { get; set; }
    }
}
