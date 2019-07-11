using Application.Command.FrolfGroupInvites.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    [TestFixture]
    public class DeleteInviteCommandValidationTests
    {
        #region Setup

        DeleteInviteCommand command;
        DeleteInviteCommandValidation handler;

        Guid inviteeId;
        FrolfGroupInvite invite;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupInvite();
            SetupMockWorkUnit();

            command = new DeleteInviteCommand(invite);
            handler = new DeleteInviteCommandValidation(mockWorkUnit.Object);
        }

        private void SetupInvite()
        {
            inviteeId = default(Guid);
            invite    = new FrolfGroupInvite
            {
                InviteeId   = inviteeId,
                Invitee     = new AppUser(),
                Inviter     = new AppUser(),
                FrolfGroup  = new FrolfGroup()
            };
        }

        private void SetupMockWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void DeleteInviteCommandValidation_PreHandle_ExpectNoException_WhenInviteeIsCurrentUser()
        {
            UserExtensions.ImpersonateUser(inviteeId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void DeleteInviteCommandValidation_PreHandle_ExpectException_WhenInviteeIsNotCurrentUser()
        {
            invite.InviteeId = Guid.NewGuid();
            UserExtensions.ImpersonateUser(inviteeId);

            Assert.Throws<Exception>(() => handler.PreHandle(command) );
        }
    }
}
