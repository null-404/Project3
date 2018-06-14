using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    /// <summary>
    /// 提醒数据模型
    /// </summary>
    public class AlertModel
    {
        public enum AlertType
        {
            /// <summary>
            /// 成功
            /// </summary>
            Success,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
            /// <summary>
            /// 错误
            /// </summary>
            Danger
        }
        /// <summary>
        /// 提醒内容
        /// </summary>
        public string Content { get; set; }

        public AlertType Level { get; set; } = AlertType.Warning;
    }
   
}
