using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Widget
{
    public class Message
    {
        public void Show(MessageModel m)
        {
            MessageException me = new MessageException
            {
                Message = m
            };
            throw me;
        }

      
    }
}
