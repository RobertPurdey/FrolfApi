using Application.Command.Players.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.Players.Conditions
{
    [TestFixture]
    public class IsPlayerCreatorOfGroupTests
    {
        #region Setup

        Player player;

        IsPlayerCreatorOfGroup condition;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();

            condition = new IsPlayerCreatorOfGroup();
        }

        public void SetupPlayer()
        {
            var creatorId = Guid.NewGuid();

            player = new Player
            {
                AppUserId  = creatorId,
                FrolfGroup = new FrolfGroup { CreatedBy = creatorId } 
            };
        }

        #endregion

        [Test]
        public void IsPlayerCreatorOfGroup_Validate_ExpectTrue_PlayerUserIdMatchesGroupCreatorId()
        {
            Assert.IsTrue( condition.Validate(player) );
        }

        [Test]
        public void IsPlayerCreatorOfGroup_Validate_ExpectFalse_WhenCurrentUserDidNotCreateGame()
        {
            player.AppUserId = Guid.NewGuid();

            Assert.IsFalse( condition.Validate(player) );
        }
    }
}
