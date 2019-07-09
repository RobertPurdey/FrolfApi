using Application.Command.FrolfGroups.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroups.Conditions
{
    [TestFixture]
    public class IsCurrentUserGroupCreatorTests
    {
        #region Setup

        Guid creatorId;
        FrolfGroup group;

        IsCurrentUserGroupCreator condition;

        [SetUp]
        public void Setup()
        {
            SetupInvite();

            condition = new IsCurrentUserGroupCreator();
        }

        private void SetupInvite()
        {
            creatorId = default(Guid);
            group     = new FrolfGroup { CreatedBy = creatorId };
        }

        #endregion

        [Test]
        public void IsCurrentUserGroupCreator_Validate_ExpectTrue_WhenCurrentUserIsGroupCreator()
        {
            UserExtensions.ImpersonateUser(creatorId);

            Assert.IsTrue( condition.Validate(group) );
        }

        [Test]
        public void IsCurrentUserGroupCreator_Validate_ExpectFalse_WhenCurrentUserNotGroupCreator()
        {
            var groupNotCreatedByCurrentUser = new FrolfGroup { CreatedBy = Guid.NewGuid() };
            UserExtensions.ImpersonateUser(creatorId);

            Assert.IsFalse( condition.Validate(groupNotCreatedByCurrentUser) );
        }
    }
}
