using Application.Command.FrolfGroups.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.FrolfGroups.Conditions
{
    [TestFixture]
    public class CanUserInviteTests
    {
        #region Setup
        
        Guid groupAdminId;
        Player groupAdmin;

        Guid groupMemberId;
        Player groupMember;

        FrolfGroup group;

        CanUserInvite condition;

        [SetUp]
        public void Setup()
        {
            SetupGroupAdmin();
            SetupGroupMember();
            SetupGroup();

            condition = new CanUserInvite(groupAdminId);
        }

        public void SetupGroupAdmin()
        {
            groupAdminId = Guid.NewGuid();

            groupAdmin = new Player
            {
                AppUserId = groupAdminId,
                GroupRole = GroupRole.Administrator
            };
        }

        public void SetupGroupMember()
        {
            groupMemberId = Guid.NewGuid();

            groupMember = new Player
            {
                AppUserId = groupMemberId,
                GroupRole = GroupRole.Member
            };
        }

        public void SetupGroup()
        {
            group = new FrolfGroup
            {
                Members = new List<Player>
                {
                    groupAdmin,
                    groupMember
                }
            };
        }

        #endregion

        [Test]
        public void CanUserInvite_Validate_ExpectTrue_WhenAppUserIdIsGroupAdmin()
        {
            Assert.IsTrue( condition.Validate(group) );
        }

        [Test]
        public void CanUserInvite_Validate_ExpectFalse_WhenAppUserIdIsMember()
        {
            condition = new CanUserInvite(groupMemberId);

            Assert.IsFalse( condition.Validate(group) );
        }

        [Test]
        public void CanUserInvite_Validate_ExpectFalse_WhenAppUserIdIsNotInGroup()
        {
            condition = new CanUserInvite(Guid.NewGuid());

            Assert.IsFalse( condition.Validate(group) );
        }
    }
}
