using Application.Command.FrolfGroupInvites.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    public class AcceptInviteCommandHandlerTests
    {
        #region Setup

        AcceptInviteCommand command;
        AcceptInviteCommandHandler handler;

        FrolfGroupInvite invite;

        Mock<IRepository<FrolfGroupInvite>> mockFrolfGRoupInviteRepo;
        Mock<IRepository<Player>> mockPlayerRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupInvite();
            SetupMockWorkUnit();

            command = new AcceptInviteCommand(invite);
            handler = new AcceptInviteCommandHandler(mockWorkUnit.Object);
        }

        private void SetupInvite()
        {
            invite = new FrolfGroupInvite
            {
                InviteeId    = Guid.NewGuid(),
                FrolfGroupId = Guid.NewGuid(),
                Invitee      = new AppUser { Handle = "Hopper" },
                Inviter      = new AppUser(),
                FrolfGroup   = new FrolfGroup()
            };
        }

        private void SetupMockWorkUnit()
        {
            mockFrolfGRoupInviteRepo = new Mock<IRepository<FrolfGroupInvite>>();
            mockPlayerRepo           = new Mock<IRepository<Player>>();

            mockWorkUnit = new Mock<IWorkUnit>();

            mockWorkUnit
                .Setup(m => m.GetRepository<FrolfGroupInvite>())
                .Returns(mockFrolfGRoupInviteRepo.Object);

            mockWorkUnit
                .Setup(m => m.GetRepository<Player>())
                .Returns(mockPlayerRepo.Object);
        }

        #endregion

        [Test]
        public void AcceptInviteCommandHandler_Handle_ExpectNoException_WhenInviteSet()
        {
            Assert.DoesNotThrow( () => handler.Handle(command) );
        }

        [Test]
        public void AcceptInviteCommandHandler_Handle_ExpectRepoCalls_WhenInviteeIsHandled()
        {
            handler.Handle(command);

            mockPlayerRepo.Verify(m => m.Add(It.IsAny<Player>()), Times.Once);
            mockFrolfGRoupInviteRepo.Verify(m => m.Remove(It.IsAny<FrolfGroupInvite>()), Times.Once);           
        }
    }
}
