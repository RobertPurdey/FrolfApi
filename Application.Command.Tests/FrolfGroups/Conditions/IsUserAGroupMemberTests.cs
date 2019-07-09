using Application.Command.FrolfGroups.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.FrolfGroups.Conditions
{
    [TestFixture]
    public class IsUserAGroupMemberTests
    {
        #region Setup

        Guid appUserId;
        FrolfGroup frolfGroup;

        IsUserAGroupMember condition;

        [SetUp]
        public void Setup()
        {
            SetupFrolfGroup();

            condition = new IsUserAGroupMember(appUserId);
        }

        private void SetupFrolfGroup()
        {
            appUserId  = default(Guid);
            frolfGroup = new FrolfGroup
            {
                Members = new List<Player>
                {
                    new Player{ AppUserId = appUserId }
                }
            };
        }

        #endregion

        [Test]
        public void IsUserAGroupMember_Validate_ExpectTrue_WhenAppUserIdIsMember()
        {           
            Assert.IsTrue( condition.Validate(frolfGroup) );
        }

        [Test]
        public void IsUserAGroupMember_Validate_ExpectFalse_WhenAppUserIdNotMember()
        {
            condition = new IsUserAGroupMember(Guid.NewGuid());

            Assert.IsFalse( condition.Validate(frolfGroup) );
        }
    }
}
