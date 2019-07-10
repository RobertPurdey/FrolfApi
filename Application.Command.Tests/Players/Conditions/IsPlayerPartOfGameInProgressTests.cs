using Application.Command.Players.Conditions;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using NUnit.Framework;
using System.Collections.Generic;

namespace Application.Command.Tests.Players.Conditions
{
    [TestFixture]
    public class IsPlayerPartOfGameInProgressTests
    {
        #region Setup

        Player player;

        IsPlayerPartOfGameInProgress condition;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();

            condition = new IsPlayerPartOfGameInProgress();
        }

        public void SetupPlayer()
        {
            player = new Player
            {
                Rounds = new List<Round>
                {
                    new Round { Game = new Game { State = GameState.InProgress } }
                }
            };
        }

        #endregion

        [Test]
        public void IsPlayerPartOfGameInProgress_Validate_ExpectTrue_PlayerHasGameInProgress()
        {
            Assert.IsTrue( condition.Validate(player) );
        }

        [Test]
        public void IsPlayerPartOfGameInProgress_Validate_ExpectFalse_WhenPlayerHasNoGamesInProgress()
        {
            player.Rounds.Clear();
            player.Rounds.Add(new Round { Game = new Game { State = GameState.Completed } });

            Assert.IsFalse( condition.Validate(player) );
        }
    }
}
