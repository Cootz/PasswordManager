using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Utils
{
    public static class EncryptionHelper
    {
        public static byte[] GenerateKey()
        {
            List<byte> key = new List<byte>();

            using Aes aes_encryption = Aes.Create();
            using Aes aes_HMAC = Aes.Create();

            aes_encryption.KeySize = 256;
            aes_HMAC.KeySize = 256;

            aes_encryption.GenerateKey();
            aes_HMAC.GenerateKey();

            key.AddRange(aes_encryption.Key);
            key.AddRange(aes_HMAC.Key);

            return key.ToArray();
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
