using Application.Command.FrolfGroups.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Application.Command.Tests.FrolfGroups.Conditions
{
    [TestFixture]
    public class DoesFrolfGroupHaveInvitesTests
    {
        #region Setup

        FrolfGroup group;
        DoesFrolfGroupHaveInvites condition;

        [SetUp]
        public void Setup()
        {
            group = new FrolfGroup
            {
                Invites = new List<FrolfGroupInvite> { new FrolfGroupInvite() }
            };

            condition = new DoesFrolfGroupHaveInvites();
        }

        #endregion

        [Test]
        public void DoesFrolfGroupHaveInvites_Validate_ExpectTrue_WhenOneInviteExists()
        {
            Assert.IsTrue( condition.Validate(group) );
        }

        [Test]
        public void DoesFrolfGroupHaveInvites_Validate_ExpectTrue_WhenManyInvitesExist()
        {
            group.Invites.Add(new FrolfGroupInvite());

            Assert.IsTrue( condition.Validate(group) );
        }

        [Test]
        public void DoesFrolfGroupHaveInvites_Validate_ExpectFalse_WhenNoInvites()
        {
            group.Invites = new List<FrolfGroupInvite>();

            Assert.IsFalse( condition.Validate(group) );
        }
    }
}
