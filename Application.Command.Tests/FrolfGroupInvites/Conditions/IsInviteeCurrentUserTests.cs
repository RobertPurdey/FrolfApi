using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Conditions
{
    [TestFixture]
    public class IsInviteeCurrentUserTests
    {
        #region Setup

        Guid inviteeId;
        FrolfGroupInvite invite;

        IsInviteeCurrentUser condition;

        [SetUp]
        public void Setup()
        {
            SetupInvite();

            condition = new IsInviteeCurrentUser();
        }

        private void SetupInvite()
        {
            inviteeId = default(Guid);
            invite    = new FrolfGroupInvite { InviteeId = inviteeId };
        }

        #endregion

        [Test]
        public void IsInviteeCurrentUser_Validate_ExpectTrue_WhenCurrentUserIsInvitee()
        {
            UserExtensions.ImpersonateUser(invite.InviteeId);

            Assert.IsTrue( condition.Validate(invite) );
        }

        [Test]
        public void IsInviteeCurrentUser_Validate_ExpectFalse_WhenCurrentUserNotInvitee()
        {
            var notInvitee = new FrolfGroupInvite { InviteeId = Guid.NewGuid() };
            UserExtensions.ImpersonateUser(invite.InviteeId);

            Assert.IsFalse( condition.Validate(notInvitee) );
        }
    }
}
