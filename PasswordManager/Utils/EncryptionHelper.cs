using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Utils;

/// <summary>
/// Contains encryption utilities
/// </summary>
public static class EncryptionHelper
{
    /// <summary>
    /// Generates key using AES-256 ecryption algorithm
    /// </summary>
    /// <returns>512 bytes (256 aes + 256 HMAC) long key</returns>
    public static byte[] GenerateKey()
    {
        List<byte> key = new();

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

    /// <summary>
    /// Convert byte array to key string
    /// </summary>
    /// <param name="key">Encryption key</param>
    /// <returns><see cref="string"/> equivalent of the <paramref name="key"/></returns>
    public static string ToKeyString(this byte[] key) => Encoding.Unicode.GetString(key);

    /// <summary>
    /// Convert key string to byte array
    /// </summary>
    /// <param name="key_string">Encryption key string</param>
    /// <returns><see cref="byte"/> <see cref="Array"/> equivalent of the <paramref name="key_string"/></returns>
    public static byte[] ToKey(this string key_string) => Encoding.Unicode.GetBytes(key_string);
}