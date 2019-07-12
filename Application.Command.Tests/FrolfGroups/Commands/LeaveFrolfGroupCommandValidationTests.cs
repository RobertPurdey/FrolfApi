using Application.Command.FrolfGroups.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.FrolfGroups.Commands
{
    [TestFixture]
    public class LeaveFrolfGroupCommandValidationTests
    {
        #region Setup

        LeaveFrolfGroupCommand command;
        LeaveFrolfGroupCommandValidation handler;

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
            SetupFrolfGroup();
            SetupWorkUnit();

            command = new LeaveFrolfGroupCommand { FrolfGroup = frolfGroup, Player = groupMember };
            handler = new LeaveFrolfGroupCommandValidation(mockWorkUnit.Object);
        }

        private void SetupPlayers()
        {
            currentUserId = default(Guid);
            creatorId     = Guid.NewGuid();

            groupMember = new Player
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

            groupCreator = new Player
            {
                AppUserId = creatorId,
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

        private void SetupFrolfGroup()
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
        public void LeaveFrolfGroupCommandValidation_PreHandle_ExpectNoExceptions_WhenRemovalIsValid()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void LeaveFrolfGroupCommandValidation_PreHandle_ExpectException_PlayerLeavingNotCurrentUser()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.Player.AppUserId = Guid.NewGuid();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void LeaveFrolfGroupCommandValidation_PreHandle_ExpectException_PlayerLeavingNotInGroup()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.FrolfGroup.Members.Clear();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void LeaveFrolfGroupCommandValidation_PreHandle_ExpectException_PlayerLeavingIsGroupCreator()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            
            groupCreator.AppUserId       = currentUserId;
            
            command.FrolfGroup.CreatedBy = currentUserId;
            command.Player               = groupCreator;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void LeaveFrolfGroupCommandValidation_PreHandle_ExpectException_PlayerLeavingIsInGameInProgress()
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
