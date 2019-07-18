using Security.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Security.Encryption
{
    public class HashManager : IHashManager
    {
        public string Hash(string input)
        {
            var sha256      = new SHA256CryptoServiceProvider();
            var inputBytes  = Encoding.UTF8.GetBytes(input);
            var outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public string Hash(string input, string salt)
        {
            return Hash(input, salt);
        }
    }
}
