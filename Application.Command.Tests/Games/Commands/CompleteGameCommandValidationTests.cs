using Application.Command.Games.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.Games.Commands
{
    [TestFixture]
    public class CompleteGameCommandValidationTests
    {
        #region Setup

        CompleteGameCommand command;
        CompleteGameCommandValidation handler;

        Game game;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupGame();
            SetupWorkUnit();

            command = new CompleteGameCommand { Game = game };
            handler = new CompleteGameCommandValidation(mockWorkUnit.Object);
        }

        private void SetupGame()
        {
            game = new Game
            {
                State = GameState.InProgress
            };
        }

        private void SetupWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void CompleteGameCommandValidation_PreHandle_ExpectNoExceptions_WhenGameValid()
        {
            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void CompleteGameCommandValidation_PreHandle_ExpectException_WhenGameAlreadyCompleted()
        {
            command.Game.State = GameState.Completed;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
