using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.AppUsers.Commands
{
    [TestFixture]
    public class AddAppUserCommandValidationTests
    {
        #region Setup

        AddAppUserCommand command;
        AddAppUserCommandValidation handler;

        AppUser newAppUser;
        AppUser existingAppUser;

        List<AppUser> dataStore;

        Mock<IWorkUnit> mockWorkUnit;
        Mock<IQueryService<AppUser>> mockAppUserQuery;       

        [SetUp]
        public void Setup()
        {
            SetupUsers();
            SetupMockAppUserQuery();
            SetupWorkUnit();

            command = new AddAppUserCommand { NewUser = newAppUser };
            handler = new AddAppUserCommandValidation(mockWorkUnit.Object, mockAppUserQuery.Object);
        }

        private void SetupUsers()
        {
            newAppUser = new AppUser
            {
                LoginName  = "Patrick",
                FriendCode = "toomuchfun",
            };

            existingAppUser = new AppUser
            {
                LoginName   = "Spongebob",
                FriendCode  = "Gary",                
            };

            dataStore = new List<AppUser> { existingAppUser };
        }

        private void SetupMockAppUserQuery()
        {
            mockAppUserQuery = new Mock<IQueryService<AppUser>>();

            mockAppUserQuery.Setup(m => m.GetAll()).Returns(dataStore.AsQueryable);
        }

        private void SetupWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void AddAppUserCommandValidation_PreHandle_ExpectNoExceptions_WhenNewUserValid()
        {
            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddAppUserCommandValidation_PreHandle_ExpectException_WhenFriendCodeInUse()
        {
            command.NewUser.FriendCode = existingAppUser.FriendCode;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddAppUserCommandValidation_PreHandle_ExpectException_WhenLoginNameInUse()
        {
            command.NewUser.LoginName = existingAppUser.LoginName;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
