using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Models
{
    public class CommentsModel
    {
        public IList<CommentJsonModel> comments { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int pageindex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int totalpages { get; set; }

        /// <summary>
        /// 内容条目总数
        /// </summary>
        public int totalcontents { get; set; }

        /// <summary>
        /// 剩余条目
        /// </summary>
        public int hascount { get; set; }
        /// <summary>
        /// 是否还有上页
        /// </summary>
        public bool haspreviouspage
        {
            get
            {
                return (pageindex > 1);
            }
        }

        /// <summary>
        /// 是否还有下页
        /// </summary>
        public bool hasnextpage
        {
            get
            {
                return (pageindex < totalpages);
            }
        }
    }
}
