using Application.Command.Courses.Conditions;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Tests.Courses.Conditions
{
    [TestFixture]
    public class DoCourseHolesMeetParRequirementTests
    {
        #region Setup
        
        Course course;
        DoCourseHolesMeetParRequirement condition;

        [SetUp]
        public void Setup()
        {
            SetupValidCourseHoles();

            condition = new DoCourseHolesMeetParRequirement();
        }

        private void SetupValidCourseHoles()
        {
            course = new Course
            {
                Holes = new List<Hole>
                {
                    new Hole { Par = 1 },
                    new Hole { Par = 2 },
                    new Hole { Par = 3 },
                }
            };
        }

        #endregion

        [Test]
        public void DoCourseHolesMeetParRequirement_Validate_ExpectTrue_WhenNoParBelowOne()
        {
            Assert.IsTrue( condition.Validate(course) );
        }

        [Test]
        public void DoCourseHolesMeetParRequirement_Validate_ExpectFalse_WhenSingleParBelowOne()
        {
            course.Holes.Add(new Hole { Par = 0 });

            Assert.IsFalse( condition.Validate(course) );
        }
    }
}
