using Application.Command.Players.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Tests.Players.Conditions
{
    [TestFixture]
    public class IsPlayerCurrentUserTests
    {
        #region Setup

        Guid playerUserId;
        Player player;

        IsPlayerCurrentUser condition;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();

            condition = new IsPlayerCurrentUser();
        }

        private void SetupPlayer()
        {
            playerUserId = default(Guid);
            player       = new Player { AppUserId = playerUserId };
        }

        #endregion

        [Test]
        public void IsPlayerCurrentUser_Validate_ExpectTrue_WhenPlayerIdMatchesCurrentUserId()
        {
            UserExtensions.ImpersonateUser(playerUserId);

            Assert.IsTrue( condition.Validate(player) );
        }

        [Test]
        public void IsPlayerCurrentUser_Validate_ExpectFalse_WhenPlayerIdDoesNotMatchCurrentUserId()
        {
            var notCurrentUser = new Player { AppUserId = Guid.NewGuid() };
            UserExtensions.ImpersonateUser(playerUserId);

            Assert.IsFalse( condition.Validate(notCurrentUser) );
        }
    }
}
