using Domain.Entities;
using Frolf.Api.Mappers.Courses;
using Frolf.Api.Models.Courses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.Tests.Mappers.Courses
{
    [TestFixture]
    public class CourseMapperTests
    {
        #region Setup
        
        CourseMapper mapper;

        Course entity;
        CourseModel model;

        Hole hole1;
        Hole hole2;

        int expectedHoleCount;
        int expectedPar;

        [SetUp]
        public void Setup()
        {
            SetupHoles();

            SetupEntity();
            SetupModel();

            mapper = new CourseMapper();

            expectedHoleCount   = 2;
            expectedPar         = 7;
        }

        private void SetupHoles()
        {
            hole1   = new Hole { EntityKey = Guid.NewGuid(), Par = 3 };
            hole2   = new Hole { EntityKey = Guid.NewGuid(), Par = 4 };
        }

        private void SetupEntity()
        {          
            entity = new Course
            {
                EntityKey    = Guid.NewGuid(),
                FrolfGroupId = Guid.NewGuid(),
                Name         = "Sandy Slopes",
                Holes        = new List<Hole> { hole1, hole2 }
            };


        }

        private void SetupModel()
        {
            model = new CourseModel
            {
                IdKey        = Guid.NewGuid(),
                FrolfGroupId = Guid.NewGuid(),
                Name         = "Dynamite Dunes"
            };
        }

        #endregion

        [Test]
        public void CourseMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new Course();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,         mappedEntity.EntityKey    );
            Assert.AreEqual( model.FrolfGroupId,  mappedEntity.FrolfGroupId );
            Assert.AreEqual( model.Name,          mappedEntity.Name         );
        }

        [Test]
        public void CourseMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new CourseModel();

            mapper.MapToApiModel(mappedModel, entity);

            Assert.AreEqual( entity.EntityKey,     mappedModel.IdKey        );
            Assert.AreEqual( entity.FrolfGroupId,  mappedModel.FrolfGroupId );
            Assert.AreEqual( entity.Name,          mappedModel.Name         );
            Assert.AreEqual( expectedPar,          mappedModel.Par          );
            Assert.AreEqual( expectedHoleCount,    mappedModel.HoleCount    );
            
            // Test hole id list in detail
            // - correct hole ids
            // - correct count
            var hasHole1   = mappedModel.HoleIds.Any(holeId => holeId == hole1.EntityKey);           
            var hasHole2   = mappedModel.HoleIds.Any(holeId => holeId == hole2.EntityKey);
            var holeCount  = mappedModel.HoleIds.Count();

            Assert.IsTrue( hasHole1 );
            Assert.IsTrue( hasHole2 );
            Assert.AreEqual( expectedHoleCount, holeCount );
        }
    }
}
