using NUnit.Framework;
using Security.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace Security.Tests.Encryption
{
    [TestFixture]
    public class RsaEncryptionTests
    {
        #region SetUp

        RsaEncryptionManager rsaEncryptor;

        [SetUp]
        public void Setup()
        {
            rsaEncryptor = new RsaEncryptionManager();
        }
        

        #endregion

        [Test]
        public void AesEncryptionManager_Encrypt_ExpectDecryptText()
        {
            var rsaKey      = new RSACryptoServiceProvider(2048).ToXmlString(true);
            var expectedMsg = "the snozberries taste like snozberries";

            var encryptedMsgBytes = rsaEncryptor.Encrypt(rsaKey, expectedMsg);
            var decryptedMsgBytes = rsaEncryptor.Decrypt(rsaKey, encryptedMsgBytes);           
            var decryptedMsg      = Encoding.UTF8.GetString(decryptedMsgBytes);

            Assert.AreEqual( expectedMsg, decryptedMsg );
        }
    }
}
