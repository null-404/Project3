using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Data.Models
{
    public class CommentJsonModel : Comments
    {
        /// <summary>
        /// 头像MD5
        /// </summary>
        public string headmd5 { get; set; }

    }
}
