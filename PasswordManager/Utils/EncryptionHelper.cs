using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Utils
{
    public static class EncryptionHelper
    {
        public static byte[] GenerateKey()
        {
            using Aes aes = Aes.Create();
            aes.KeySize = 512;
            aes.GenerateKey();

            return aes.Key;
        }

        public static string ToKeyString(this byte[] key)
        {
            return Encoding.UTF8.GetString(key);
        }

        public static byte[] ToKey(this string key_string)
        { 
            return Encoding.UTF8.GetBytes(key_string);
        }
    }
}
