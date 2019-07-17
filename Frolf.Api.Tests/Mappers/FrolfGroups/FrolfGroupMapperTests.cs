using Domain.Entities;
using Frolf.Api.Mappers.FrolfGroups;
using Frolf.Api.Models.FrolfGroups;
using NUnit.Framework;
using System;

namespace Frolf.Api.Tests.Mappers.FrolfGroups
{
    [TestFixture]
    public class FrolfGroupMapperTests
    {
        #region Setup
        
        FrolfGroupMapper mapper;

        FrolfGroup entity;
        FrolfGroupModel model;

        [SetUp]
        public void Setup()
        {
            SetupEntity();
            SetupModel();

            mapper = new FrolfGroupMapper();
        }

        private void SetupEntity()
        {          
            entity = new FrolfGroup
            {
                EntityKey    = Guid.NewGuid(),
                Name         = "dudes n' bros",
                CreatedDate  = DateTime.UtcNow
            };


        }

        private void SetupModel()
        {
            model = new FrolfGroupModel
            {
                IdKey    = Guid.NewGuid(),
                Name     = "The Brady Bunch",
            };
        }

        #endregion

        [Test]
        public void FrolfGroupMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new FrolfGroup();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,  mappedEntity.EntityKey );
            Assert.AreEqual( model.Name,   mappedEntity.Name      );
        }

        [Test]
        public void FrolfGroupMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new FrolfGroupModel();

            mapper.MapToApiModel(mappedModel, entity);
             
            Assert.AreEqual( entity.EntityKey,  mappedModel.IdKey        );
            Assert.AreEqual( entity.Name,       mappedModel.Name         );
            Assert.AreEqual( entity.CreatedDate, mappedModel.CreatedDate );
        }
    }
}
