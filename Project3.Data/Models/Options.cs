using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project3.Data.Models
{
    /// <summary>
    /// 配置数据实体模型
    /// </summary>
    public class Options
    {
        /*
         * adminusername 管理账号
         * adminpassword 管理密码
         * title 网站标题
         * subtitle 子标题
         * allowcomments 是否允许评论（0是，1否）
         * logo LOGO为空时显示网站标题
         * siteurl 网站Url
         */
        /// <summary>
        /// 配置名
        /// </summary>
        [Key]
        public string name { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string value { get; set; }
    }
}
