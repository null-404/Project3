using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Areas.Admin.Models
{
    public class ResultModel
    {
        /// <summary>
        /// 受影响数
        /// </summary>
        public int affecteds { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool issuccess { get; set; }

        /// <summary>
        /// 备注内容
        /// </summary>
        public string remark { get; set; }
    }
}
