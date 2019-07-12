using Application.Command.Players.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.Players
{
    [TestFixture]
    public class RemovePlayerCommandValidationTests
    {
        #region Setup

        RemovePlayerCommand command;
        RemovePlayerCommandValidation handler;

        Guid currentUserId;        
        Player groupMember;

        Guid creatorId;
        Player groupCreator;

        FrolfGroup frolfGroup;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupPlayers();
            SetupPlayer();
            SetupWorkUnit();

            command = new RemovePlayerCommand { FrolfGroup = frolfGroup, Player = groupMember };
            handler = new RemovePlayerCommandValidation(mockWorkUnit.Object);
        }

        private void SetupPlayers()
        {
            currentUserId = default(Guid);
            creatorId     = currentUserId;

            groupMember = new Player
            {
                AppUserId = Guid.NewGuid(),
                GroupRole = GroupRole.Member,
                
                Rounds = new List<Round>
                {
                    new Round
                    {
                        Game = new Game { State = GameState.Completed }
                    }
                }
            };

            groupCreator = new Player
            {
                AppUserId = currentUserId,
                GroupRole = GroupRole.Member,

                Rounds = new List<Round>
                {
                    new Round
                    {
                        Game = new Game { State = GameState.Completed }
                    }
                }
            };
        }

        private void SetupPlayer()
        {
            frolfGroup = new FrolfGroup
            {
                Name      = "dudes n' bros",
                CreatedBy = creatorId,

                Members = new List<Player>
                {
                    groupMember, groupCreator
                }
            };

            groupMember.FrolfGroup  = frolfGroup;
            groupCreator.FrolfGroup = frolfGroup;
        }

        private void SetupWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void RemovePlayerCommandValidation_PreHandle_ExpectNoExceptions_WhenRemovalIsValid()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void RemovePlayerCommandValidation_PreHandle_ExpectException_CurrentUserNotGroupCreator()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.FrolfGroup.CreatedBy = Guid.NewGuid();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void RemovePlayerCommandValidation_PreHandle_ExpectException_PlayerBeingRemovedNotInGroup()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.FrolfGroup.Members.Clear();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void RemovePlayerCommandValidation_PreHandle_ExpectException_PlayerBeingRemovedNotGroupCreator()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.Player = groupCreator;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void RemovePlayerCommandValidation_PreHandle_ExpectException_PlayerBeingRemovedInGameInProgress()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            groupMember.Rounds = new List<Round>
            {
                new Round { Game = new Game { State = GameState.InProgress } }
            };

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
