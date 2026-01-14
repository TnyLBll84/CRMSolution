using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class CryptoService
    {
        //Testing Code
        //byte[] salt = RandomNumberGenerator.GetBytes(16);
        //string cipherText = cryptoService.Encrypt("hi every one", "P@ssw0rd", salt);
        //string plainText = cryptoService.Decrypt(cipherText, "P@ssw0rd", salt);

        public string Encrypt(string plainText, string password, byte[] salt)
        {

            var rbg = new Rfc2898DeriveBytes(password, salt);



            var algorithm = Aes.Create();
            var rbgKey = rbg.GetBytes(algorithm.KeySize / 8);
            var rbgIV = rbg.GetBytes(algorithm.BlockSize / 8);


            var encryptor = algorithm.CreateEncryptor(rbgKey, rbgIV);

            var bufferStream = new MemoryStream();

            var cryptoStream = new CryptoStream(bufferStream, encryptor, CryptoStreamMode.Write);

            var bytesToTransform = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(bytesToTransform, 0, bytesToTransform.Length);
            cryptoStream.FlushFinalBlock();

            cryptoStream.Close();
            bufferStream.Close();

            return Convert.ToBase64String(bufferStream.ToArray());

        }

        public string Decrypt(string cipherText, string password, byte[] salt)
        {

            var rbg = new Rfc2898DeriveBytes(password, salt);



            var algorithm = Aes.Create();
            var rbgKey = rbg.GetBytes(algorithm.KeySize / 8);
            var rbgIV = rbg.GetBytes(algorithm.BlockSize / 8);


            var encryptor = algorithm.CreateDecryptor(rbgKey, rbgIV);

            var bufferStream = new MemoryStream();

            var cryptoStream = new CryptoStream(bufferStream, encryptor, CryptoStreamMode.Write);

            var bytesToTransform = Convert.FromBase64String(cipherText);
            cryptoStream.Write(bytesToTransform, 0, bytesToTransform.Length);
            cryptoStream.FlushFinalBlock();

            cryptoStream.Close();
            bufferStream.Close();

            return Encoding.UTF8.GetString(bufferStream.ToArray());

        }
    }
}

