using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Models.Content
{
    public class IndexViewModel : BaseViewModel
    {
        /// <summary>
        /// 内容
        /// </summary>
        public Contents content { get; set; }

        /// <summary>
        /// 分类和标签
        /// </summary>
        public IList<Metas> metas { get; set; }
    }
}
