using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project3.Data.Models
{
    /// <summary>
    /// 内容数据实体模型
    /// </summary>
    public class Contents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 标识id
        /// </summary>
        public int cid { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string excerpt { get; set; }
        
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 类型（0：博客，1：页面，2：文件）
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime updatetime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 状态（0显示，1隐藏）
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 是否允许评论（0是，1否）
        /// </summary>
        public int allowcomment { get; set; }

        /// <summary>
        /// 头图
        /// </summary>
        public string banner { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public int parent { get; set; }

        /// <summary>
        /// 缩略名
        /// </summary>
        public string slug { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int commentsnum { get; set; }

        /// <summary>
        /// 阅读数
        /// </summary>
        public int readnum { get; set; }
    }
}
