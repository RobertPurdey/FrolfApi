using Application.Command.AppUsers.Conditions;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.AppUsers.Conditions
{
    [TestFixture]
    public class DoesAppUserExistTests
    {
        #region Setup
        
        Guid appUserId;
        AppUser existingUser;

        DoesAppUserExist condition;
        
        List<AppUser> appUserDataStore;

        Mock<IQueryService<AppUser>> mockAppUserQuery;

        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupDataStore();
            SetupMockQuery();

            condition = new DoesAppUserExist(mockAppUserQuery.Object);
        }

        private void SetupUser()
        {
            appUserId    = Guid.NewGuid();
            existingUser = new AppUser { EntityKey = appUserId };
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
        public void DoesAppUserExist_Validate_ExpectTrue_WhenMatchingUser()
        {
            Assert.IsTrue( condition.Validate(existingUser) );
        }

        [Test]
        public void DoesAppUserExist_Validate_ExpectFalse_WhenNoMatchingUser()
        {
            var nonExistantUser = new AppUser { EntityKey = Guid.NewGuid() };

            Assert.IsFalse( condition.Validate(nonExistantUser) );
        }
    }
}
