using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Project3.Extensions
{
    public class MD5Extension
    {
        public static string Encrypt(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PassEncrypt(string str)
        {
            return Encrypt(Encrypt(str) + "Project3");
        }
    }
}
