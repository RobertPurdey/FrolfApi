using Application.Command.Courses.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Application.Command.Tests.Courses.Conditions
{
    [TestFixture]
    public class IsCourseHolesOrderSequencedTests
    {
        #region Setup
        
        Course course;
        IsCourseHolesOrderSequenced condition;

        [SetUp]
        public void Setup()
        {
            SetupValidCourseHoles();

            condition = new IsCourseHolesOrderSequenced();
        }

        private void SetupValidCourseHoles()
        {
            course = new Course
            {
                Holes = new List<Hole>
                {
                    new Hole { Order = 1 },
                    new Hole { Order = 3 },
                    new Hole { Order = 2 },
                }
            };
        }

        #endregion

        [Test]
        public void IsCourseHolesOrderSequenced_Validate_ExpectTrue_WhenOrderSequencedStartingAtOne()
        {
            Assert.IsTrue( condition.Validate(course) );
        }

        [Test]
        public void IsCourseHolesOrderSequenced_Validate_ExpectFalse_WhenSequenceDoesntStartAtOneButSequenced()
        {
            course.Holes.Clear();
            course.Holes.Add(new Hole { Order = 2 });
            course.Holes.Add(new Hole { Order = 3 });

            Assert.IsFalse( condition.Validate(course) );
        }

        [Test]
        public void IsCourseHolesOrderSequenced_Validate_ExpectFalse_WhenSequenceStartAtOneButNotSequenced()
        {
            course.Holes.Clear();
            course.Holes.Add(new Hole { Order = 1 });
            course.Holes.Add(new Hole { Order = 3 });

            Assert.IsFalse( condition.Validate(course) );
        }
    }
}
