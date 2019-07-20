using NUnit.Framework;
using Security.Encryption;

namespace Security.Tests.Encryption
{
    [TestFixture]
    public class AesEncryptionTests
    {
        #region SetUp

        AesEncryptionManager aesEncryptor;

        [SetUp]
        public void Setup()
        {
            aesEncryptor = new AesEncryptionManager();
        }
        

        #endregion

        [Test]
        public void AesEncryptionManager_GenerateKey_ExpectKeyLength_24()
        {
            var key = aesEncryptor.GenerateKey();

            Assert.AreEqual(24, key.Length);
        }

        [Test]
        public void AesEncryptionManager_Encrypt_ExpectDecryptText()
        {
            var key         = aesEncryptor.GenerateKey();
            var expectedMsg = "the snozberries taste like snozberries";

            var encrpytedMsg = aesEncryptor.Encrypt(key, expectedMsg);
            var decryptedMsg = aesEncryptor.Decrypt(key, encrpytedMsg);

            Assert.AreNotEqual( expectedMsg, encrpytedMsg  );
            Assert.AreEqual( expectedMsg, decryptedMsg);
        }

        [Test]
        public void AesEncryptionManager_Encrypt_CanDecryptLongText()
        {
            var key         = aesEncryptor.GenerateKey();
            var expectedMsg = "Soldiers! Don't give yourselves to brutes - men who despise you and enslave you - who"
                            + " regiment your lives - tell you what to do - what to think and what to feel! Who drill you -"
                            + " diet you - treat you like cattle, use you as cannon fodder. Don't give yourselves to these"
                            + " unnatural men - machine men with machine minds and machine hearts! You are not"
                            + " machines! You are not cattle! You are men! You have the love of humanity in your hearts."
                            + " You don't hate, only the unloved hate - the unloved and the unnatural!"

                            + "Soldiers! Don't fight for slavery! Fight for liberty! In the seventeenth chapter of St Luke,"
                            + " it is written the kingdom of God is within man not one man nor a group of men, but in all"
                            + " men! In you! You, the people, have the power - the power to create machines. The power"
                            + " to create happiness! You, the people, have the power to make this life free and beautiful"
                            + " - to make this life a wonderful adventure. Then in the name of democracy - let us use"
                            + " that power - let us all unite. Let us fight for a new world - a decent world that will give"
                            + " men a chance to work - that will give youth a future and old age a security.";

            var encrpytedMsg = aesEncryptor.Encrypt(key, expectedMsg);
            var decryptedMsg = aesEncryptor.Decrypt(key, encrpytedMsg);

            Assert.AreNotEqual(expectedMsg, encrpytedMsg);
            Assert.AreEqual(expectedMsg, decryptedMsg);
        }
    }
}
