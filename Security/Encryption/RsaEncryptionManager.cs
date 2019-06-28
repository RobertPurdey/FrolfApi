using Security.Contracts;
using System.Security.Cryptography;
using System;
using System.Text;

namespace Security.Encryption
{
    public class RsaEncryptionManager : IRsaEncryptionManager
    {
        public byte[] Encrypt(string xmlKey, string msg)
        {
            var encryptedMsg = new byte[]{ };

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(xmlKey);
                encryptedMsg = rsa.Encrypt(Encoding.UTF8.GetBytes(msg), false);
            }

            return encryptedMsg;
        }

        public byte[] Decrypt(string xmlKey, byte[] encryptedMsg)
        {
            var decryptedBytes = new byte[] { };

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(xmlKey);
                decryptedBytes = rsa.Decrypt(encryptedMsg, false);
            }

            return decryptedBytes;
        }
    }
}
