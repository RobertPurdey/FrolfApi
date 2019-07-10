using Application.Command.Games.Conditions;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using NUnit.Framework;

namespace Application.Command.Tests.Games.Conditions
{
    [TestFixture]
    public class IsGameInProgressTests
    {
        #region Setup

        Game game;

        IsGameInProgress condition;

        [SetUp]
        public void Setup()
        {
            SetupValidGame();

            condition = new IsGameInProgress();
        }

        private void SetupValidGame()
        {
            game = new Game { State = GameState.InProgress };
        }

        #endregion

        [Test]
        public void IsGameInProgress_Validate_ExpectTrue_GameInProgress()
        {
            Assert.IsTrue( condition.Validate(game) );
        }

        [Test]
        public void IsGameInProgress_Validate_ExpectFalse_GameIsCompleted()
        {
            game.State = GameState.Completed;

            Assert.IsFalse( condition.Validate(game) );
        }
    }
}
