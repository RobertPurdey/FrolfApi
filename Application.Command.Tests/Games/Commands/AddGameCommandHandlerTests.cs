using Application.Command.Games.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.Command.Tests.Games.Commands
{
    [TestFixture]
    public class AddGameCommandHandlerTests
    {
        #region Setup

        AddGameCommand command;
        AddGameCommandHandler handler;

        Game newGame;

        Mock<IRepository<Game>> mockGameRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupNewUser();
            SetupWorkUnit();

            command = new AddGameCommand { NewGame = newGame };
            handler = new AddGameCommandHandler(mockWorkUnit.Object);
        }

        private void SetupNewUser()
        {
            newGame = new Game
            {
                Name = "Game of Throws",
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
        public void AddGameCommandHandler_Handle_NewGameAddedToRepo()
        {
            handler.Handle(command);

            mockGameRepo.Verify(m => m.Add(newGame), Times.Once);
        }
    }
}
