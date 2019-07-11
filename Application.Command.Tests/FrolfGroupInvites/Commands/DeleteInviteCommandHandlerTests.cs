using Application.Command.FrolfGroupInvites.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    [TestFixture]
    public class DeleteInviteCommandHandlerTests
    {
        #region Setup

        DeleteInviteCommand command;
        DeleteInviteCommandHandler handler;

        FrolfGroupInvite invite;

        Mock<IRepository<FrolfGroupInvite>> mockFrolfGRoupInviteRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupInvite();
            SetupMockWorkUnit();

            command = new DeleteInviteCommand(invite);
            handler = new DeleteInviteCommandHandler(mockWorkUnit.Object);
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
        public void DeleteInviteCommandHandler_Handle_ExpectNoException_WhenInviteSet()
        {
            Assert.DoesNotThrow( () => handler.Handle(command) );
        }

        [Test]
        public void DeleteInviteCommandHandler_Handle_ExpectRepoCalls_WhenInviteeIsHandled()
        {
            handler.Handle(command);

            mockFrolfGRoupInviteRepo.Verify(m => m.Remove(It.IsAny<FrolfGroupInvite>()), Times.Once);           
        }
    }
}
