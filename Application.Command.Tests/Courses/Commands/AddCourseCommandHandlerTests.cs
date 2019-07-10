using Application.Command.Courses.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.Courses.Commands
{
    [TestFixture]
    public class AddCourseCommandHandlerTests
    {
        #region Setup

        AddCourseCommand command;
        AddCourseCommandHandler handler;

        Course newCourse;

        Mock<IRepository<Course>> mockCourseRepo;
        Mock<IRepository<Hole>> mockHoleRepo;

        Mock<IWorkUnit> mockWorkUnit;     
        
        [SetUp]
        public void Setup()
        {
            SetupNewCourse();
            SetupWorkUnit();

            command = new AddCourseCommand { newEntity = newCourse };
            handler = new AddCourseCommandHandler(mockWorkUnit.Object);
        }

        private void SetupNewCourse()
        {
            newCourse = new Course
            {
                Holes = new List<Hole> {new Hole(), new Hole() }
            };
        }

        private void SetupWorkUnit()
        { 
            mockCourseRepo  = new Mock<IRepository<Course>>();
            mockHoleRepo    = new Mock<IRepository<Hole>>();
            mockWorkUnit    = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<Course>()).Returns(mockCourseRepo.Object);
            mockWorkUnit.Setup(m => m.GetRepository<Hole>()).Returns(mockHoleRepo.Object);
        }

        #endregion

        [Test]
        public void AddCourseCommandHandler_Handle_NewCourseAddedToRepo()
        {
            handler.Handle(command);

            mockCourseRepo.Verify( m => m.Add(It.IsAny<Course>()), Times.Once );
        }

        [Test]
        public void AddCourseCommandHandler_Handle_HolesAddedWithCourseId()
        {
            handler.Handle(command);

            mockHoleRepo.Verify( m => m.Add(It.IsAny<Hole>()), Times.Exactly(2) );

            var allHolesHaveCourseId = command.newEntity.Holes.All(h => h.CourseId == command.newEntity.EntityKey);

            Assert.IsTrue( allHolesHaveCourseId );
        }
    }
}
