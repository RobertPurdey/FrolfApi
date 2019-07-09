using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Entities;
using NUnit.Framework;

namespace Application.Command.Tests.FrolfGroupInvites.Conditions
{
    [TestFixture]
    public class IsInviteNullTests
    {
        #region Setup

        IsInviteNull condition;

        [SetUp]
        public void Setup()
        {
            condition = new IsInviteNull();
        }

        #endregion

        [Test]
        public void IsInviteNull_Validate_ExpectTrue_WhenNull()
        {
            FrolfGroupInvite invite = null;

            Assert.IsTrue( condition.Validate(invite) );
        }

        [Test]
        public void IsInviteNull_Validate_ExpectFalse_WhenNotNull()
        {
            FrolfGroupInvite invite = new FrolfGroupInvite();

            Assert.IsFalse( condition.Validate(invite) );
        }
    }
}
