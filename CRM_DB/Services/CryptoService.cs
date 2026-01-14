using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CRM_DB
{
    internal class CryptoService
    {
        public string Encrypt(string plainText, string password, byte[] salt)
        {
            var rbg = new Rfc2898DeriveBytes(password, salt);

            using var algorithm = Aes.Create();
            var rbgKey = rbg.GetBytes(algorithm.KeySize / 8);
            var rbgIV = rbg.GetBytes(algorithm.BlockSize / 8);

            using var encryptor = algorithm.CreateEncryptor(rbgKey, rbgIV);
            using var bufferStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(bufferStream, encryptor, CryptoStreamMode.Write);

            var bytesToTransform = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(bytesToTransform, 0, bytesToTransform.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(bufferStream.ToArray());
        }

        public string Decrypt(string cipherText, string password, byte[] salt)
        {
            var rbg = new Rfc2898DeriveBytes(password, salt);

            var algorithm = Aes.Create();
            var rbgKey = rbg.GetBytes(algorithm.KeySize / 8);
            var rbgIV = rbg.GetBytes(algorithm.BlockSize / 8);

            var decryptor = algorithm.CreateDecryptor(rbgKey, rbgIV);

            var bufferStream = new MemoryStream();
            var cryptoStream = new CryptoStream(bufferStream, decryptor, CryptoStreamMode.Write);

            var bytesToTransform = Convert.FromBase64String(cipherText);
            cryptoStream.Write(bytesToTransform, 0, bytesToTransform.Length);
            cryptoStream.FlushFinalBlock();

            cryptoStream.Close();
            bufferStream.Close();

            return Encoding.UTF8.GetString(bufferStream.ToArray());
        }
    }
}
