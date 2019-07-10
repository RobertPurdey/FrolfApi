using Application.Command.Games.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.Games.Conditions
{
    [TestFixture]
    public class IsGameCreatedByCurrentUserTests
    {
        #region Setup

        Guid appUserId;
        Game game;

        IsGameCreatedByCurrentUser condition;

        [SetUp]
        public void Setup()
        {
            SetupValidGame();

            condition = new IsGameCreatedByCurrentUser();
        }

        private void SetupValidGame()
        {
            appUserId = default(Guid);

            game = new Game { CreatedBy = appUserId };
        }

        #endregion

        [Test]
        public void IsGameCreatedByCurrentUser_Validate_ExpectTrue_WhenCurrentUserCreatedGame()
        {
            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsTrue( condition.Validate(game) );
        }

        [Test]
        public void IsGameCreatedByCurrentUser_Validate_ExpectFalse_WhenCurrentUserDidNotCreateGame()
        {
            game.CreatedBy = Guid.NewGuid();
            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsFalse( condition.Validate(game) );
        }

        [Test]
        public void IsGameCreatedByCurrentUser_Validate_ExpectFalse_WhenGameIsNull()
        {
            game = null;

            Assert.IsFalse( condition.Validate(game) );
        }
    }
}
