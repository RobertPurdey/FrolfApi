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
    public class IsCourseHolesOrderUniqueTests
    {
        #region Setup
        
        Course course;
        IsCourseHolesOrderUnique condition;

        [SetUp]
        public void Setup()
        {
            SetupValidCourseHoles();

            condition = new IsCourseHolesOrderUnique();
        }

        private void SetupValidCourseHoles()
        {
            course = new Course
            {
                Holes = new List<Hole>
                {
                    new Hole { Order = 1 },
                    new Hole { Order = 2 },
                    new Hole { Order = 3 },
                }
            };
        }

        #endregion

        [Test]
        public void IsCourseHolesOrderUnique_Validate_ExpectTrue_WhenOrderIsUnique()
        {
            Assert.IsTrue( condition.Validate(course) );
        }

        [Test]
        public void IsCourseHolesOrderUnique_Validate_ExpectFalse_WhenOrderNotUnique()
        {
            course.Holes.Add(new Hole { Order = 2 });

            Assert.IsFalse( condition.Validate(course) );
        }
    }
}
