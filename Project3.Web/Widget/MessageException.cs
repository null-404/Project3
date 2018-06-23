using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Widget
{
    public class MessageException : ApplicationException
    {
        public new MessageModel Message { get; set; }

    
    }
}
