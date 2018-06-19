using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Models.ContentVM
{
    public class ArticleViewModel : BaseViewModel
    {
        /// <summary>
        /// 内容
        /// </summary>
        public Contents content { get; set; }

        /// <summary>
        /// 分类和标签
        /// </summary>
        public IList<Metas> metas { get; set; }

        //public IList<Comments> comments { get; set; }
    }
}
