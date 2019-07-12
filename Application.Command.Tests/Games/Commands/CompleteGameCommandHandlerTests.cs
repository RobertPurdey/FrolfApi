using Application.Command.Games.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Tests.Games.Commands
{
    [TestFixture]
    public class CompleteGameCommandHandlerTests
    {
        #region Setup

        CompleteGameCommand command;
        CompleteGameCommandHandler handler;

        Game game;

        Mock<IRepository<Game>> mockGameRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupNewUser();
            SetupWorkUnit();

            command = new CompleteGameCommand { Game = game };
            handler = new CompleteGameCommandHandler(mockWorkUnit.Object);
        }

        private void SetupNewUser()
        {
            game = new Game
            {
                Name  = "Boss Toss",
                State = GameState.InProgress
            };
        }

        private void SetupWorkUnit()
        {
            mockGameRepo = new Mock<IRepository<Game>>();
            mockWorkUnit = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<Game>()).Returns(mockGameRepo.Object);
        }

        #endregion

        [Test]
        public void CompleteGameCommandHandler_Handle_NewGameCompleteedToRepo()
        {
            handler.Handle(command);

            mockGameRepo.Verify(m => m.Update(game), Times.Once);
            Assert.AreEqual(GameState.Completed, command.Game.State);
        }
    }
}
