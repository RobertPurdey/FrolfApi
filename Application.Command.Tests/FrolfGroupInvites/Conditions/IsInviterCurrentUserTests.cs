using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Conditions
{
    [TestFixture]
    public class IsInviterCurrentUserTests
    {
        #region Setup

        Guid inviterId;
        FrolfGroupInvite invite;

        IsInviterCurrentUser condition;

        [SetUp]
        public void Setup()
        {
            SetupInvite();

            condition = new IsInviterCurrentUser();
        }

        private void SetupInvite()
        {
            inviterId = default(Guid);
            invite    = new FrolfGroupInvite { InviterId = inviterId };
        }

        #endregion

        [Test]
        public void IsInviterCurrentUser_Validate_ExpectTrue_WhenCurrentUserIsInvitee()
        {
            UserExtensions.ImpersonateUser(invite.InviterId);

            Assert.IsTrue( condition.Validate(invite) );
        }

        [Test]
        public void IsInviterCurrentUser_Validate_ExpectFalse_WhenCurrentUserNotInvitee()
        {
            var notInviter = new FrolfGroupInvite { InviterId = Guid.NewGuid() };
            UserExtensions.ImpersonateUser(invite.InviterId);

            Assert.IsFalse( condition.Validate(notInviter) );
        }
    }
}
