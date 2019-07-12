using Application.Command.FrolfGroups.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroups.Commands
{
    [TestFixture]
    public class LeaveFrolfGroupCommandHandlerTests
    {
        #region Setup

        LeaveFrolfGroupCommand command;
        LeaveFrolfGroupCommandHandler handler;

        Player playerLeaving;

        Mock<IRepository<Player>> mockPlayerRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();
            SetupWorkUnit();

            command = new LeaveFrolfGroupCommand { FrolfGroup = null, Player = playerLeaving };
            handler = new LeaveFrolfGroupCommandHandler(mockWorkUnit.Object);
        }

        private void SetupPlayer()
        {
            playerLeaving = new Player { EntityKey = Guid.NewGuid() };
        }

        private void SetupWorkUnit()
        {
            mockPlayerRepo  = new Mock<IRepository<Player>>();
            mockWorkUnit    = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<Player>()).Returns(mockPlayerRepo.Object);
        }

        #endregion

        [Test]
        public void LeaveFrolfGroupCommandHandler_Handle_LeavingPlayerRemoved()
        {
            handler.Handle(command);

            mockPlayerRepo.Verify(
                m => m.Remove(It.Is<Player>(p => p.EntityKey == playerLeaving.EntityKey)),
                Times.Once
            );
        }
    }
}
