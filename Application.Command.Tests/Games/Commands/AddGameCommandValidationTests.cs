using Application.Command.Games.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.Games.Commands
{
    [TestFixture]
    public class AddGameCommandValidationTests
    {
        #region Setup

        AddGameCommand command;
        AddGameCommandValidation handler;

        Guid currentUserId;

        Player currentPlayer;
        Player playerA;
        Player playerB;

        Game newGame;
        FrolfGroup frolfGroup;
        Course course;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupPlayers();
            SetupFrolfGroup();
            SetupCourse();
            SetupGame();
            SetupWorkUnit();

            command = new AddGameCommand { NewGame = newGame };
            handler = new AddGameCommandValidation(mockWorkUnit.Object);
        }

        private void SetupPlayers()
        {
            currentUserId = default(Guid);
            currentPlayer = new Player { AppUserId = currentUserId };
            playerA       = new Player { AppUserId = Guid.NewGuid() };
            playerB       = new Player { AppUserId = Guid.NewGuid() };
        }

        private void SetupFrolfGroup()
        {
            frolfGroup = new FrolfGroup()
            {
                EntityKey = Guid.NewGuid(),

                Members = new List<Player>
                {
                    currentPlayer, playerA, playerB
                }
            };
        }

        private void SetupCourse()
        {
            course = new Course
            {
                EntityKey    = Guid.NewGuid(),
                FrolfGroupId = frolfGroup.EntityKey
            };
        }

        private void SetupGame()
        {
            newGame = new Game
            {
                Name         = "dudes n' bros",
                FrolfGroup   = frolfGroup,
                FrolfGroupId = frolfGroup.EntityKey,
                Course       = course,

                Rounds = new List<Round>
                {
                    new Round { Player = playerA },
                    new Round { Player = playerB }
                }
            };
        }

        private void SetupWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void AddGameCommandValidation_PreHandle_ExpectNoExceptions_WhenGameValid()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddGameCommandValidation_PreHandle_ExpectException_WhenNameNull()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.NewGame.Name = null;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddGameCommandValidation_PreHandle_ExpectException_WhenNameEmpty()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.NewGame.Name = "";

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddGameCommandValidation_PreHandle_ExpectException_WhenCurrentUserNotInGroup()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            // remove current user from group
            command.NewGame.FrolfGroup.Members.Clear();
            command.NewGame.FrolfGroup.Members.Add(playerA);
            command.NewGame.FrolfGroup.Members.Add(playerB);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddGameCommandValidation_PreHandle_ExpectException_WhenPlayerInGameButNotGroup()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            command.NewGame.Rounds.Add(new Round
            {
                Player = new Player { EntityKey = Guid.NewGuid() }
            });

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddGameCommandValidation_PreHandle_ExpectException_WhenCourseNotForGroup()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.NewGame.Course.FrolfGroupId = Guid.NewGuid();

            Assert.Throws<Exception>(() => handler.PreHandle(command));
        }
    }
}
