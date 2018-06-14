using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Data.Service.Interface
{
    public interface ICache
    {
        object Get(string name);

        void Set(string name, object value);

        void Remove(string name);

        //void Update(string name, object value);
    }
}
