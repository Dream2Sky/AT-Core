using System;
using System.Security.Cryptography;
using System.Text;

namespace AT_Core.Common
{
    public class EncryptUtils
    {
        public static string GetMD5(string srcStr)
        {
            using (var md5 = MD5.Create())
            {
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(srcStr)));
            }
        }
    }
}