using Application.Command.HoleScores.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.HoleScores.Conditions
{
    [TestFixture]
    public class AreHoleScoresForSameGameTests
    {
        #region Setup

        List<HoleScore> holeScores;

        AreHoleScoresForSameGame condition;

        [SetUp]
        public void Setup()
        {
            SetupValidHoleScores();

            condition = new AreHoleScoresForSameGame();
        }

        private void SetupValidHoleScores()
        {
            var gameId = Guid.NewGuid();

            holeScores = new List<HoleScore>
            {
                new HoleScore { GameId = gameId },
                new HoleScore { GameId = gameId }
            };
        }

        #endregion

        [Test]
        public void AreHoleScoresForSameGame_Validate_ExpectTrue_AllHoleScoresShareGameId()
        {
            Assert.IsTrue( condition.Validate(holeScores) );
        }

        [Test]
        public void AreHoleScoresForSameGame_Validate_ExpectFalse_WhenHoleScoresGameIdsDiffer()
        {
            holeScores.Add(new HoleScore { GameId = Guid.NewGuid() });

            Assert.IsFalse( condition.Validate(holeScores) );
        }
    }
}
