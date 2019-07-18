using Frolf.Api.Composers.Users;
using Frolf.Api.Models.Users;
using Moq;
using NUnit.Framework;
using Security.Contracts;
using System;

namespace Frolf.Api.Tests.Composers.Users
{
    [TestFixture]
    public class UserComposerTests
    {
        #region Setup

        UserComposer composer;

        AppUserCreationModel creationModel;

        string expectedHashedPassword;
        string expectedFriendCode;
        string expectedSalt;

        Mock<IHashManager> mockHashManager;
        Mock<IFriendCodeGenerator> mockFriendCodeGenerator;
        Mock<ISaltShaker> mockSaltShaker;

        [SetUp]
        public void Setup()
        {
            SetupMockHashManager();
            SetupMockFriendCodeGenerator();
            SetupMockSaltShaker();

            SetupCreationModel();

            composer = new UserComposer(
                mockHashManager.Object,
                mockFriendCodeGenerator.Object,
                mockSaltShaker.Object );
        }

        private void SetupMockHashManager()
        {
            mockHashManager        = new Mock<IHashManager>();
            expectedHashedPassword = "i'm cuckoo for cocoa puffs";

            mockHashManager
                .Setup( m => m.Hash( It.IsAny<string>(), It.IsAny<string>() ) )
                .Returns( expectedHashedPassword );
        }

        private void SetupMockFriendCodeGenerator()
        {
            mockFriendCodeGenerator = new Mock<IFriendCodeGenerator>();
            expectedFriendCode      = "milk and a bowl";

            mockFriendCodeGenerator
                .Setup(m => m.Generate() )
                .Returns( expectedFriendCode );

        }

        private void SetupMockSaltShaker()
        {
            mockSaltShaker = new Mock<ISaltShaker>();
            expectedSalt   = "salt in cereal? gross";

            mockSaltShaker
                .Setup( m => m.Shake( It.IsAny<int>() ) )
                .Returns( expectedSalt );
        }


        private void SetupCreationModel()
        {
            creationModel = new AppUserCreationModel
            {
                LoginName       = "Sonny",
                Handle          = "Cuckoo Bird",
                Password        = "i'm cuckoo for cocoa puffs",
                ConfirmPassword = "i'm cuckoo for cocoa puffs"
            };
        }

        #endregion

        [Test]
        public void UserComposer_NewAppUser_ValuesAreSet()
        {
            var newUser = composer.NewAppUser( creationModel );

            Assert.AreEqual( creationModel.LoginName,  newUser.LoginName  );
            Assert.AreEqual( creationModel.Handle,     newUser.Handle     );
            Assert.AreEqual( expectedHashedPassword,   newUser.Password   );
            Assert.AreEqual( expectedFriendCode,       newUser.FriendCode );
            Assert.AreEqual( expectedSalt,             newUser.Salt       );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenPasswordsDontMathc()
        {
            creationModel.ConfirmPassword = "i'm cocoa puffs for cuckoo";

            Assert.Throws<Exception>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullLoginNameNull()
        {
            creationModel.LoginName = null;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullLoginNameEmpty()
        {
            creationModel.LoginName = string.Empty;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullHandleNull()
        {
            creationModel.Handle = null;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullHandleEmpty()
        {
            creationModel.Handle = string.Empty;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullPasswordNull()
        {
            creationModel.Password = null;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullPasswordEmpty()
        {
            creationModel.Password = string.Empty;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullConfirmPasswordNull()
        {
            creationModel.ConfirmPassword = null;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }

        [Test]
        public void UserComposer_NewAppUser_ExpectException_WhenNullConfirmPasswordEmpty()
        {
            creationModel.ConfirmPassword = string.Empty;

            Assert.Throws<ArgumentNullException>( () => composer.NewAppUser(creationModel) );
        }
    }
}
