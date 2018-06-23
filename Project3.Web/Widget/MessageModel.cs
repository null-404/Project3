using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Project3.Web.Widget
{

    public class MessageModel 
    {

        //public bool IsShow { get; set; } = false;
        public string Title { get; set; } = "提示";
        public string Content { get; set; } = "遇到了一个未知问题";
    }
}
