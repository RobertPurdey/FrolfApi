using Application.Command.Players.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.Players
{
    [TestFixture]
    public class RemovePlayerCommandHandlerTests
    {
        #region Setup

        RemovePlayerCommand command;
        RemovePlayerCommandHandler handler;

        Player playerBeingRemoved;

        Mock<IRepository<Player>> mockPlayerRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();
            SetupWorkUnit();

            command = new RemovePlayerCommand { FrolfGroup = null, Player = playerBeingRemoved };
            handler = new RemovePlayerCommandHandler(mockWorkUnit.Object);
        }

        private void SetupPlayer()
        {
            playerBeingRemoved = new Player { EntityKey = Guid.NewGuid() };
        }

        private void SetupWorkUnit()
        {
            mockPlayerRepo  = new Mock<IRepository<Player>>();
            mockWorkUnit    = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<Player>()).Returns(mockPlayerRepo.Object);
        }

        #endregion

        [Test]
        public void RemovePlayerCommandHandler_Handle_LeavingPlayerRemoved()
        {
            handler.Handle(command);

            mockPlayerRepo.Verify(
                m => m.Remove(It.Is<Player>(p => p.EntityKey == playerBeingRemoved.EntityKey)),
                Times.Once
            );
        }
    }
}
