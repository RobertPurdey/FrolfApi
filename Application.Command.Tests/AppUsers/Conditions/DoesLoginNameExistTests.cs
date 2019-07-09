using Application.Command.AppUsers.Conditions;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.AppUsers.Conditions
{
    [TestFixture]
    public class DoesLoginNameExistTests
    {
        #region Setup
        
        string loginName;
        AppUser existingUser;

        DoesLoginNameExist condition;
        
        List<AppUser> appUserDataStore;

        Mock<IQueryService<AppUser>> mockAppUserQuery;

        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupDataStore();
            SetupMockQuery();

            condition = new DoesLoginNameExist(mockAppUserQuery.Object);
        }

        private void SetupUser()
        {
            loginName    = "Mugen";
            existingUser = new AppUser { LoginName = loginName };
        }

        private void SetupDataStore()
        {
            appUserDataStore = new List<AppUser> { existingUser };
        }

        private void SetupMockQuery()
        {
            mockAppUserQuery = new Mock<IQueryService<AppUser>>();

            mockAppUserQuery
                .Setup( m => m.GetAll() )
                .Returns( appUserDataStore.AsQueryable );
        }

        #endregion

        [Test]
        public void DoesLoginNameExist_Validate_ExpectTrue_WhenMatchingLoginName()
        {
            Assert.IsTrue( condition.Validate(existingUser) );
        }

        [Test]
        public void DoesLoginNameExist_Validate_ExpectFalse_WhenNoMatchingLoginName()
        {
            var nonExistantUser = new AppUser { FriendCode = "Jin" };

            Assert.IsFalse( condition.Validate(nonExistantUser) );
        }
    }
}
