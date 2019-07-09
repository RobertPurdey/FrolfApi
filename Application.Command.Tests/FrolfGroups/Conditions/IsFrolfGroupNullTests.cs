using Application.Command.FrolfGroups.Conditions;
using Domain.Entities;
using NUnit.Framework;

namespace Application.Command.Tests.FrolfGroups.Conditions
{
    [TestFixture]
    public class IsInviteNullTests
    {
        #region Setup

        IsFrolfGroupNull condition;

        [SetUp]
        public void Setup()
        {
            condition = new IsFrolfGroupNull();
        }

        #endregion

        [Test]
        public void IsFrolfGroupNull_Validate_ExpectTrue_WhenNull()
        {
            FrolfGroup group = null;

            Assert.IsTrue( condition.Validate(group) );
        }

        [Test]
        public void IsFrolfGroupNull_Validate_ExpectFalse_WhenNotNull()
        {
            FrolfGroup group = new FrolfGroup();

            Assert.IsFalse( condition.Validate(group) );
        }
    }
}
