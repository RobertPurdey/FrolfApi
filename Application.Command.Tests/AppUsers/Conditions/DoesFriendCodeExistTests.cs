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
    public class DoesFriendCodeExistTests
    {
        #region Setup
        
        string friendCode;
        AppUser existingUser;

        DoesFriendCodeExist condition;
        
        List<AppUser> appUserDataStore;

        Mock<IQueryService<AppUser>> mockAppUserQuery;

        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupDataStore();
            SetupMockQuery();

            condition = new DoesFriendCodeExist(mockAppUserQuery.Object);
        }

        private void SetupUser()
        {
            friendCode   = "friends4life";
            existingUser = new AppUser { FriendCode = friendCode };
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
        public void DoesFriendCodeExist_Validate_ExpectTrue_WhenMatchingFriendCode()
        {
            Assert.IsTrue( condition.Validate(existingUser) );
        }

        [Test]
        public void DoesFriendCodeExist_Validate_ExpectFalse_WhenNoMatchingFriendCode()
        {
            var nonExistantUser = new AppUser { FriendCode = "nobodyHome" };

            Assert.IsFalse( condition.Validate(nonExistantUser) );
        }
    }
}
