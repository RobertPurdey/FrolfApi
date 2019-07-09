using Application.Command.AppUsers.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.AppUsers.Conditions
{
    [TestFixture]
    public class IsAppUserCurrentUserTests
    {
        #region Setup

        Guid appUserId;
        AppUser appUser;

        IsAppUserCurrentUser condition;

        [SetUp]
        public void Setup()
        {
            SetupUser();

            condition = new IsAppUserCurrentUser();
        }

        private void SetupUser()
        {
            appUserId = default(Guid);
            appUser   = new AppUser { EntityKey = appUserId };
        }

        #endregion

        [Test]
        public void IsAppUserCurrentUser_Validate_ExpectTrue_WhenCurrentUserIsAppUser()
        {
            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsTrue( condition.Validate(appUser) );
        }

        [Test]
        public void IsAppUserCurrentUser_Validate_ExpectFalse_WhenCurrentUserNotAppUser()
        {
            var notCurrUser = new AppUser { EntityKey = Guid.NewGuid() };
            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsFalse( condition.Validate(notCurrUser) );
        }
    }
}
