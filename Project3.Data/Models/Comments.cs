using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project3.Data.Models
{
    /// <summary>
    /// 评论数据实体模型
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int coid { get; set; }

        /// <summary>
        /// 内容id
        /// </summary>
        public int cid { get; set; }

        [Required(ErrorMessage ="邮箱地址不能为空")]
        [EmailAddress(ErrorMessage ="邮箱地址格式错误")]
        /// <summary>
        /// 邮箱
        /// </summary>
        public string mail { get; set; }

        [Required(ErrorMessage ="昵称不能为空")]
        [StringLength(maximumLength:10,ErrorMessage ="昵称不能超过10字符")]
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 父级评论id
        /// </summary>
        public int parentcoid { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime { get; set; }

        [Required(ErrorMessage ="评论内容不能为空")]
        [StringLength(maximumLength:500,ErrorMessage ="评论内容不能超过500字符")]
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }


    }
}
