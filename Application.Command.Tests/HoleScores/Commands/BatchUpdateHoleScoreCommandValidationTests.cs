using Application.Command.HoleScores.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.HoleScores.Commands
{
    [TestFixture]
    public class BatchUpdateHoleScoreCommandValidationTests
    {
        #region Setup

        BatchUpdateHoleScoreCommand command;
        BatchUpdateHoleScoreCommandValidation handler;

        Guid currentUserId;
        Guid gameId;
        Game game;

        List<HoleScore> holeScores;
        List<Game> gameDataStore;

        Mock<IQueryService<Game>> mockGameQuery;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupGame();
            SetupGameDataStore();
            SetupMockGameQuery();
            SetupHoleScores();
            SetupMockWorkUnit();

            command = new BatchUpdateHoleScoreCommand(game, gameId, holeScores);
            handler = new BatchUpdateHoleScoreCommandValidation(mockWorkUnit.Object, mockGameQuery.Object);
        }

        private void SetupUser()
        {
            currentUserId = default(Guid);
        }

        private void SetupGame()
        {
            gameId = Guid.NewGuid();
           
            game = new Game
            { 
                EntityKey = gameId,
                CreatedBy = currentUserId,
                State     = GameState.InProgress
               
            };
        }

        private void SetupGameDataStore()
        {
            gameDataStore = new List<Game> { game };
        }

        private void SetupMockGameQuery()
        {
            mockGameQuery = new Mock<IQueryService<Game>>();

            mockGameQuery.Setup(m => m.GetAll()).Returns(gameDataStore.AsQueryable);
        }

        private void SetupHoleScores()
        {
            holeScores = new List<HoleScore>
            {
                new HoleScore { GameId = gameId, Strokes = 1 },
                new HoleScore { GameId = gameId, Strokes = 2 }
            };
        }

        private void SetupMockWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void BatchUpdateHoleScoreCommandValidation_PreHandle_ExpectNoException_WhenHolesValid()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void BatchUpdateHoleScoreCommandValidation_PreHandle_ExpectException_WhenGameCompleted()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            game.State = GameState.Completed;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void BatchUpdateHoleScoreCommandValidation_PreHandle_ExpectException_WhenNoHolesMatchGame()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            command.HolesToUpdate = new List<HoleScore>
            {
                new HoleScore { GameId = Guid.NewGuid(), Strokes = 1 }
            };

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void BatchUpdateHoleScoreCommandValidation_PreHandle_ExpectException_WhenStrokesBelowOne()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            command.HolesToUpdate = new List<HoleScore>
            {
                new HoleScore { GameId = Guid.NewGuid(), Strokes = 0 }
            };

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void BatchUpdateHoleScoreCommandValidation_PreHandle_ExpectException_WhenHolesForDifferentGames()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            command.HolesToUpdate = new List<HoleScore>
            {
                new HoleScore { GameId = gameId,         Strokes = 1 },
                new HoleScore { GameId = Guid.NewGuid(), Strokes = 1 }
            };

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void BatchUpdateHoleScoreCommandValidation_PreHandle_ExpectException_WhenCurrentUserNotGameCreator()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            
            game.CreatedBy = Guid.NewGuid();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
