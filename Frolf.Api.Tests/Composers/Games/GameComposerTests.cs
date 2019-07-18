using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Composers.Games;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frolf.Api.Tests.Composers.Games
{
    [TestFixture]
    public class GameComposerTests
    {
        #region Setup

        GameComposer composer;

        List<Course> courseDataStore;
        List<FrolfGroup> frolfGroupDataStore;

        Mock<IQueryService<Course>> mockCourseQuery;
        Mock<IQueryService<FrolfGroup>> mockFrolfGroupQuery;

        [SetUp]
        public void Setup()
        {
            SetupCourses();
            SetupCourseDataStore();

            SetupFrolfGroup();
            SetupFrolfGroupDataStore();

            SetupMockCourseQuery();
            SetupMockFrolfGroupQuery();

            composer = new GameComposer(mockCourseQuery.Object, mockFrolfGroupQuery.Object);
        }

        private void SetupCourses()
        {

        }

        private void SetupCourseDataStore()
        {

        }

        private void SetupFrolfGroup()
        {

        }

        private void SetupFrolfGroupDataStore()
        {

        }

        private void SetupMockCourseQuery()
        {
            mockCourseQuery = new Mock<IQueryService<Course>>();
        }

        private void SetupMockFrolfGroupQuery()
        {
            mockFrolfGroupQuery = new Mock<IQueryService<FrolfGroup>>();
        }

        #endregion
    }
}
