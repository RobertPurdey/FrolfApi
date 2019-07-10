using Application.Command.Courses.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.Courses.Commands
{
    [TestFixture]
    public class UpdateCourseCommandValidationTests
    {
        #region Setup

        UpdateCourseCommand command;
        UpdateCourseCommandValidation handler;

        Course course;

        Guid appUserId;
        AppUser appUser;

        Guid frolfGroupId;
        FrolfGroup existingFrolfGroup;

        List<FrolfGroup> frolfGroupDataStore;

        Mock<IQueryService<FrolfGroup>> mockFrolfGroupQuery;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupFrolfGroupDataStore();
            SetupMockWorkUnit();
            SetupMockFrolfGroupQuery();
            SetupValidCourse();

            command = new UpdateCourseCommand { entity = course };
            handler = new UpdateCourseCommandValidation(mockFrolfGroupQuery.Object, mockWorkUnit.Object);
        }

        private void SetupUser()
        {
            appUserId  = default(Guid);
            appUser    = new AppUser { EntityKey = appUserId };
        }

        private void SetupFrolfGroupDataStore()
        {
            frolfGroupId       = Guid.NewGuid();
            existingFrolfGroup = new FrolfGroup
            {
                EntityKey = frolfGroupId,
                Members   = new List<Player> { new Player { AppUserId = appUserId } }
            };

            frolfGroupDataStore = new List<FrolfGroup> { existingFrolfGroup };
        }

        private void SetupMockWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        private void SetupMockFrolfGroupQuery()
        {
            mockFrolfGroupQuery = new Mock<IQueryService<FrolfGroup>>();

            mockFrolfGroupQuery.Setup(m => m.GetAll()).Returns(frolfGroupDataStore.AsQueryable);
        }

        private void SetupValidCourse()
        {
            course = new Course
            {
                Name         = "Queen E",
                FrolfGroupId = frolfGroupId,
                Holes        = new List<Hole>
                {
                    new Hole { Par = 1, Order = 1 },
                    new Hole { Par = 2, Order = 2 },
                    new Hole { Par = 3, Order = 3 },
                }
            };
        }

        #endregion

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectNoExpection_WhenCourseValid()
        {
            UserExtensions.ImpersonateUser(appUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenCourseNameNull()
        {
            command.entity.Name = null;
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenCourseNameEmpty()
        {
            command.entity.Name = string.Empty;
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenNullFrolfGroup()
        {
            command.entity.FrolfGroupId = Guid.NewGuid();
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenCurrentUserNotInGroup()
        {
            existingFrolfGroup.Members.Clear();
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenCourseHolesNull()
        {
            command.entity.Holes = null;
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenParUnderRequirement()
        {
            command.entity.Holes.Clear();
            command.entity.Holes.Add(new Hole { Par = 0, Order = 1 });

            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenOrderNotUnique()
        {
            command.entity.Holes.Add(new Hole { Par = 1, Order = 1 });
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateCourseCommandValidation_PreHandle_ExpectExpection_WhenNotInSequence()
        {
            command.entity.Holes.Add(new Hole { Par = 1, Order = 1337 });
            UserExtensions.ImpersonateUser(appUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
