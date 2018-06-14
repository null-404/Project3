using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    public class ContentEditViewModel
    {
        /// <summary>
        /// 选中分类、标签
        /// </summary>
        public IList<Metas> selectedmetas { get; set; }

        /// <summary>
        /// 所有分类、标签
        /// </summary>
        public IList<Metas> metas { get; set; }

        public IList<Files> files { get; set; }

        public Contents content { get; set; }

        /// <summary>
        /// 类型（0博客，1页面）
        /// </summary>
        public int Type { get; set; }
    }
}
