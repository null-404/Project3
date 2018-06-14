using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project3.Data.Models
{
    /// <summary>
    /// 分类和标签数据实体模型
    /// </summary>
    public class Metas
    {
        /// <summary>
        /// 标识Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int mid { get; set; }

        /// <summary>
        /// 分类/标签名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 类型（0分类，1标签）
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 内容条数
        /// </summary>
        public int count { get; set; }
    }
}
