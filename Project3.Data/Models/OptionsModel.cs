using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Data.Models
{
    public class OptionsModel
    {
        /// <summary>
        /// 站点标题
        /// </summary>
        public string title { get; set; } = "Project3~";
        /// <summary>
        /// 站点副标题
        /// </summary>
        public string subtitle { get; set; } = "project3 on .net core";
        /// <summary>
        /// 站点logo
        /// </summary>
        public string logo { get; set; } = "/images/logo.png";
        /// <summary>
        /// 站点URL
        /// </summary>
        public string siteurl { get; set; }
        /// <summary>
        /// 站点描述
        /// </summary>
        public string description { get; set; } = "project3 on .net core";
        /// <summary>
        /// 站点关键字
        /// </summary>
        public string keywords { get; set; } = "project3";
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public string allowcomments { get; set; } = "0";
        /// <summary>
        /// 文章列表条数
        /// </summary>
        public string postslistsize { get; set; } = "10";
        /// <summary>
        /// 评论列表条数
        /// </summary>
        public string commentslistsize { get; set; } = "10";

        /// <summary>
        /// 主题
        /// </summary>
        public string theme { get; set; } = "apollo";

        /// <summary>
        /// 评论提交冷却时间（单位秒）
        /// </summary>
        public string postcommentscd { get; set; } = "60";
    }
}
