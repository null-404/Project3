using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Extensions
{
    public class FileExtension
    {
        public static string CountSize(long size)
        {
            string m_strSize = "";

            if (size < 1024.00)
                m_strSize = size.ToString("F2") + " Byte";
            else if (size >= 1024.00 && size < 1048576)
                m_strSize = (size / 1024.00).ToString("F2") + " K";
            else if (size >= 1048576 && size < 1073741824)
                m_strSize = (size / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (size >= 1073741824)
                m_strSize = (size / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }
    }
}
