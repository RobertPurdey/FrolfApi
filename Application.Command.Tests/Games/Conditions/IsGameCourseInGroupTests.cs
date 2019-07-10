using Application.Command.Games.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.Games.Conditions
{
    [TestFixture]
    public class IsGameCourseInGroupTests
    {
        #region Setup

        Guid frolfGroupId;
        Game game;

        IsGameCourseInGroup condition;

        [SetUp]
        public void Setup()
        {
            SetupValidGame();

            condition = new IsGameCourseInGroup();
        }

        private void SetupValidGame()
        {
            frolfGroupId = Guid.NewGuid();

            game = new Game
            {
                FrolfGroupId = frolfGroupId,
                Course       = new Course { FrolfGroupId = frolfGroupId }
            };
        }

        #endregion

        [Test]
        public void IsGameCourseInGroup_Validate_ExpectTrue_MatchingFrolfGroupId()
        {
            Assert.IsTrue( condition.Validate(game) );
        }

        [Test]
        public void IsGameCourseInGroup_Validate_ExpectFalse_NoMatchingFrolfGroupId()
        {
            game.FrolfGroupId = Guid.NewGuid();

            Assert.IsFalse( condition.Validate(game) );
        }
    }
}
