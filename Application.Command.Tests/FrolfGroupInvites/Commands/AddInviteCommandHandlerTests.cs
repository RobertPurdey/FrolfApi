using Application.Command.FrolfGroupInvites.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    [TestFixture]
    public class AddInviteCommandHandlerTests
    { 
        #region Setup

        AddInviteCommand command;
        AddInviteCommandHandler handler;

        FrolfGroupInvite invite;

        Mock<IRepository<FrolfGroupInvite>> mockFrolfGRoupInviteRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupInvite();
            SetupMockWorkUnit();

            command = new AddInviteCommand(invite);
            handler = new AddInviteCommandHandler(mockWorkUnit.Object);
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
            mockWorkUnit             = new Mock<IWorkUnit>();

            mockWorkUnit
                .Setup(m => m.GetRepository<FrolfGroupInvite>())
                .Returns(mockFrolfGRoupInviteRepo.Object);
        }

        #endregion

        [Test]
        public void AddInviteCommandHandler_Handle_FrolfGroupInviteRepoCalled()
        {
            handler.Handle(command);

            mockFrolfGRoupInviteRepo.Verify(m => m.Add(It.IsAny<FrolfGroupInvite>()), Times.Once);
        }
    }
}
