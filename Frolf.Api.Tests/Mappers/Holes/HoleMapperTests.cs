using Domain.Entities;
using Frolf.Api.Mappers.Holes;
using Frolf.Api.Models.Holes;
using NUnit.Framework;
using System;

namespace Frolf.Api.Tests.Mappers.Holes
{
    [TestFixture]
    public class HoleMapperTests
    {
        #region Setup
        
        HoleMapper mapper;

        Hole entity;
        HoleModel model;

        [SetUp]
        public void Setup()
        {
            SetupEntity();
            SetupModel();

            mapper = new HoleMapper();
        }

        private void SetupEntity()
        {          
            entity = new Hole
            {
                EntityKey = Guid.NewGuid(),
                CourseId  = Guid.NewGuid(),
                Order     = 1,
                Par       = 3
            };
        }

        private void SetupModel()
        {
            model = new HoleModel
            {
                IdKey     = Guid.NewGuid(),
                CourseId  = Guid.NewGuid(),
                Order     = 1,
                Par       = 3
            };
        }

        #endregion

        [Test]
        public void HoleMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new Hole();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,     mappedEntity.EntityKey );
            Assert.AreEqual( model.CourseId,  mappedEntity.CourseId  );
            Assert.AreEqual( model.Order,     mappedEntity.Order     );
            Assert.AreEqual( model.Par,       mappedEntity.Par       );
        }

        [Test]
        public void HoleMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new HoleModel();

            mapper.MapToApiModel(mappedModel, entity);

            Assert.AreEqual( entity.EntityKey,  mappedModel.IdKey    );
            Assert.AreEqual( entity.CourseId,   mappedModel.CourseId );
            Assert.AreEqual( entity.Order,      mappedModel.Order    );
            Assert.AreEqual( entity.Par,        mappedModel.Par      );
        }
    }
}
