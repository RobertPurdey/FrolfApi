using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.AppUsers.Commands
{
    [TestFixture]
    public class UpdateAppUserCommandValidationTests
    {
        #region Setup

        UpdateAppUserCommand command;
        UpdateAppUserCommandValidation handler;

        Guid appUserId;
        AppUser updatedAppUser;

        Mock<IWorkUnit> mockWorkUnit;   

        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupWorkUnit();

            command = new UpdateAppUserCommand { User = updatedAppUser };
            handler = new UpdateAppUserCommandValidation(mockWorkUnit.Object);
        }

        private void SetupUser()
        {
            appUserId       = default(Guid);
            updatedAppUser  = new AppUser { EntityKey = appUserId };
        }

        private void SetupWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void UpdateAppUserCommandValidation_PreHandle_ExpectNoExceptions_WhenNewUserValid()
        {
            UserExtensions.ImpersonateUser(appUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateAppUserCommandValidation_PreHandle_ExpectException_WhenFriendCodeInUse()
        {
            command.User = new AppUser { EntityKey = Guid.NewGuid() };
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
