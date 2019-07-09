using Application.Command.FrolfGroups.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.FrolfGroups.Conditions
{
    [TestFixture]
    public class DoesFrolfGroupOnlyContainCurrentUserTests
    {
        #region Setup

        Guid appUserId;
        FrolfGroup frolfGroup;

        DoesFrolfGroupOnlyContainCurrentUser condition;

        [SetUp]
        public void Setup()
        {
            SetupFrolfGroup();

            condition = new DoesFrolfGroupOnlyContainCurrentUser();
        }

        private void SetupFrolfGroup()
        {
            appUserId  = default(Guid);
            frolfGroup = new FrolfGroup
            {
                Members = new List<Player>
                {
                    new Player
                    {
                        AppUserId = appUserId,
                        GroupRole = GroupRole.Administrator
                    }
                }
            };
        }

        #endregion

        [Test]
        public void DoesFrolfGroupOnlyContainCurrentUser_Validate_ExpectTrue_WhenCurrentUserOnlyOne()
        {
            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsTrue( condition.Validate(frolfGroup) );
        }

        [Test]
        public void IsInviterCurrentUser_Validate_ExpectFalse_WhenCurrentUserNotOnlyUser()
        {
            frolfGroup.Members.Add(new Player { AppUserId = Guid.NewGuid() });
            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsFalse( condition.Validate(frolfGroup) );
        }

        [Test]
        public void IsInviterCurrentUser_Validate_ExpectFalse_WhenCurrentUserNotAdmin()
        {
            frolfGroup.Members = new List<Player>
            {
                new Player
                {
                    AppUserId = appUserId,
                    GroupRole = GroupRole.Member
                }
            };

            UserExtensions.ImpersonateUser(appUserId);

            Assert.IsFalse( condition.Validate(frolfGroup) );
        }
    }
}
