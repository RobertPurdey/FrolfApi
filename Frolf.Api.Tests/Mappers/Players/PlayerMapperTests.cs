using Domain.Entities;
using Frolf.Api.Mappers.Players;
using Frolf.Api.Models.Players;
using NUnit.Framework;
using System;

namespace Frolf.Api.Tests.Mappers.Players
{
    [TestFixture]
    public class PlayerMapperTests
    {
        #region Setup
        
        PlayerMapper mapper;

        Player entity;
        PlayerModel model;

        [SetUp]
        public void Setup()
        {
            SetupEntity();
            SetupModel();

            mapper = new PlayerMapper();
        }

        private void SetupEntity()
        {          
            entity = new Player
            {
                EntityKey    = Guid.NewGuid(),
                AppUserId    = Guid.NewGuid(),
                FrolfGroupId = Guid.NewGuid(),
                GroupRole    = GroupRole.Administrator,
                Handle       = "The Mortiest Morty"
            };


        }

        private void SetupModel()
        {
            model = new PlayerModel
            {
                IdKey        = Guid.NewGuid(),
                AppUserId    = Guid.NewGuid(),
                FrolfGroupId = Guid.NewGuid(),
                GroupRole    = GroupRole.Administrator,
                Handle       = "The Rickest Rick"
            };
        }

        #endregion

        [Test]
        public void PlayerMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new Player();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,         mappedEntity.EntityKey    );
            Assert.AreEqual( model.AppUserId,     mappedEntity.AppUserId    );
            Assert.AreEqual( model.FrolfGroupId,  mappedEntity.FrolfGroupId );
            Assert.AreEqual( model.GroupRole,     mappedEntity.GroupRole    );
            Assert.AreEqual( model.Handle,        mappedEntity.Handle       );
        }

        [Test]
        public void PlayerMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new PlayerModel();

            mapper.MapToApiModel(mappedModel, entity);
             
            Assert.AreEqual( entity.EntityKey,     mappedModel.IdKey        );
            Assert.AreEqual( entity.AppUserId,     mappedModel.AppUserId    );
            Assert.AreEqual( entity.FrolfGroupId,  mappedModel.FrolfGroupId );
            Assert.AreEqual( entity.GroupRole,     mappedModel.GroupRole    );
            Assert.AreEqual( entity.Handle,        mappedModel.Handle       );
        }
    }
}
