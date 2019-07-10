using Application.Command.Games.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.Games.Conditions
{
    #region Setup

    [TestFixture]
    public class IsGameMembersOnlyTests
    {
        Guid memberAId;
        Player memberA;

        Guid memberBId;
        Player memberB;

        Guid nonMemberAId;
        Player nonMemberA;
        Game game;

        IsGameMembersOnly condition;

        [SetUp]
        public void Setup()
        {
            SetupPlayers();
            SetupValidGame();

            condition = new IsGameMembersOnly();
        }

        private void SetupPlayers()
        {
            memberAId    = Guid.NewGuid();
            memberA      = new Player { EntityKey = memberAId };

            memberBId    = Guid.NewGuid();
            memberB      = new Player { EntityKey = memberBId };

            nonMemberAId = Guid.NewGuid();
            nonMemberA   = new Player { EntityKey = nonMemberAId };
        }

        private void SetupValidGame()
        {
            game = new Game
            { 
                Rounds = new List<Round>
                {
                    new Round { Player = memberA },
                    new Round { Player = memberB },
                },
                FrolfGroup = new FrolfGroup
                {
                    Members = new List<Player> { memberA, memberB }
                }
            };
        }

        #endregion

        [Test]
        public void IsGameMembersOnly_Validate_ExpectTrue_WhenMemberOnly()
        {
            Assert.IsTrue( condition.Validate(game) );
        }

        [Test]
        public void IsGameMembersOnly_Validate_ExpectFalse_WhenNotMembersOnly()
        {
            game.Rounds.Add(new Round { Player = nonMemberA });

            Assert.IsFalse( condition.Validate(game) );
        }
    }
}
