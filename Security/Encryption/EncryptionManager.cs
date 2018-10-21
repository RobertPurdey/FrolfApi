using Security.Contracts;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Security.Encryption
{
    public class EncryptionManager : IEncryptionManager
    {
        private const string KEY_STRING = "robertpurdey89";
        private readonly byte[] privateKey;

        public EncryptionManager()
        {
            privateKey = GenerateKeyHash();
        }

        public string Hash(string input)
        {
            var sha256      = new SHA256CryptoServiceProvider();
            var inputBytes  = Encoding.UTF8.GetBytes(input);
            var outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public string Encrypt(string input)
        {
            using (var aes = GetAesProvider())
            {
                // Initialize the AES crypto provider
                aes.GenerateIV();

                var iv  = aes.IV;
                var key = privateKey;

                var inputBytes = Encoding.UTF8.GetBytes(input);

                using (var encrypter    = aes.CreateEncryptor(key, iv))
                using (var cipherStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        //Prepend IV to data
                        cipherStream.Write(iv, 0, 16);
                        binaryWriter.Write(inputBytes);
                        cryptoStream.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(cipherStream.ToArray());
                }
            }
        }

        public string Decrypt(string input)
        {
            using (var aes = GetAesProvider())
            {
                var inputBytes = Convert.FromBase64String(input);

                //get first 16 bytes of IV and use it to decrypt
                var iv = new byte[16];
                Array.Copy(inputBytes, 0, iv, 0, iv.Length);

                using (var cipherStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(cipherStream, aes.CreateDecryptor(aes.Key, iv), CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        //Decrypt Cipher Text from Message
                        binaryWriter.Write(inputBytes, iv.Length, inputBytes.Length - iv.Length);
                    }

                    return Encoding.Default.GetString(cipherStream.ToArray());
                }
            }
        }

        private AesCryptoServiceProvider GetAesProvider()
        {
            return new AesCryptoServiceProvider
            {
                Key      = privateKey,
                Mode     = CipherMode.CBC,
                Padding  = PaddingMode.PKCS7
            };
        }

        private static byte[] GenerateKeyHash()
        {
            return new SHA256CryptoServiceProvider()
                .ComputeHash( Encoding.UTF8.GetBytes(KEY_STRING) );
        }
    }
}
