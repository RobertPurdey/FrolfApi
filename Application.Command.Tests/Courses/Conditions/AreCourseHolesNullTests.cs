using Application.Command.Courses.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Application.Command.Tests.Courses.Conditions
{
    [TestFixture]
    public class AreCourseHolesNullTests
    {
        #region Setup

        AreCourseHolesNull condition;

        [SetUp]
        public void Setup()
        {
            condition = new AreCourseHolesNull();
        }

        #endregion

        [Test]
        public void AreCourseHolesNull_Validate_ExpectTrue_WhenNull()
        {
            Course course = new Course { Holes = null };

            Assert.IsTrue( condition.Validate(course) );
        }

        [Test]
        public void AreCourseHolesNull_Validate_ExpectFalse_WhenNotNull()
        {
            Course course = new Course { Holes = new List<Hole>() };

            Assert.IsFalse( condition.Validate(course) );
        }
    }
}
