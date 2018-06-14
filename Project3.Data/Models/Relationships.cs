using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project3.Data.Models
{
    /// <summary>
    /// 内容与分类/标签的关系
    /// </summary>
    public class Relationships
    {

        /// <summary>
        /// 标识id
        /// </summary>
        [Key]
        public int rid { get; set; }
        /// <summary>
        /// 内容标识id
        /// </summary>
        public int cid { get; set; }

        /// <summary>
        /// 分类/标签标识id
        /// </summary>
        public int mid { get; set; }
    }
}
