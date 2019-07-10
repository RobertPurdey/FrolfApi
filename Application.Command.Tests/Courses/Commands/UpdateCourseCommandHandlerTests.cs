using Application.Command.Courses.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.Command.Tests.Courses.Commands
{
    [TestFixture]
    class UpdateCourseCommandHandlerTests
    {
        #region Setup

        UpdateCourseCommand command;
        UpdateCourseCommandHandler handler;

        Course newCourse;

        Mock<IRepository<Course>> mockCourseRepo;
        Mock<IWorkUnit> mockWorkUnit;     
        
        [SetUp]
        public void Setup()
        {
            SetupCourse();
            SetupWorkUnit();

            command = new UpdateCourseCommand { entity = newCourse };
            handler = new UpdateCourseCommandHandler(mockWorkUnit.Object);
        }

        private void SetupCourse()
        {
            newCourse = new Course { };
        }

        private void SetupWorkUnit()
        { 
            mockCourseRepo  = new Mock<IRepository<Course>>();
            mockWorkUnit    = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<Course>()).Returns(mockCourseRepo.Object);
        }

        #endregion

        [Test]
        public void AddCourseCommandHandler_Handle_NewCourseAddedToRepo()
        {
            handler.Handle(command);

            mockCourseRepo.Verify( m => m.Update(It.IsAny<Course>()), Times.Once );
        }
    }
}