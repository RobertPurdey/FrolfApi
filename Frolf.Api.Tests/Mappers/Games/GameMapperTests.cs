using Domain.Entities;
using Domain.Entities.Entities.Games;
using Frolf.Api.Mappers.Games;
using Frolf.Api.Models.Games;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.Tests.Mappers.Games
{
    [TestFixture]
    public class GameMapperTests
    {
        #region Setup
        
        GameMapper mapper;

        Game entity;
        GameModel model;

        Hole hole1;
        Hole hole2;

        Course course;
        FrolfGroup group;

        int expectedHoleCount;

        [SetUp]
        public void Setup()
        {
            SetupHoles();
            SetupCourse();
            SetupFrolfGroup();

            SetupEntity();
            SetupModel();

            mapper = new GameMapper();

            expectedHoleCount = 2;
        }

        private void SetupHoles()
        {
            hole1 = new Hole { EntityKey = Guid.NewGuid(), Par = 3, Order = 1 };
            hole2 = new Hole { EntityKey = Guid.NewGuid(), Par = 4, Order = 2 };
        }

        private void SetupCourse()
        {
            course = new Course
            {
                Name      = "Pender",
                Holes     = new List<Hole> { hole1, hole2 }
            };
        }

        private void SetupFrolfGroup()
        {
            group = new FrolfGroup
            {
                Name = "dudes n bros",
            };
        }

        private void SetupEntity()
        {          
            entity = new Game
            {
                EntityKey    = Guid.NewGuid(),
                FrolfGroupId = Guid.NewGuid(),
                CourseId     = Guid.NewGuid(),
                CreatedBy    = Guid.NewGuid(),
                CreatedDate  = new DateTime(2018, 4, 20),
                Name         = "Pender 2018",
                State        = GameState.Completed,
                Course       = course,
                FrolfGroup   = group
            };


        }

        private void SetupModel()
        {
            model = new GameModel
            {
                IdKey = Guid.NewGuid()
            };
        }

        #endregion

        [Test]
        public void GameMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new Game();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,  mappedEntity.EntityKey );
        }

        [Test]
        public void GameMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new GameModel();

            mapper.MapToApiModel(mappedModel, entity);

            Assert.AreEqual( entity.EntityKey,        mappedModel.IdKey      );
            Assert.AreEqual( entity.FrolfGroupId,     mappedModel.GroupId    );
            Assert.AreEqual( entity.Name,             mappedModel.Name       );
            Assert.AreEqual( entity.Course.Name,      mappedModel.CourseName );
            Assert.AreEqual( entity.FrolfGroup.Name,  mappedModel.GroupName  );
            Assert.AreEqual( entity.State,            mappedModel.State      );
            
            // Test hole id list in detail
            // - correct hole ids
            // - correct count
            var hasHole1   = mappedModel.HoleIds.Any( holeId => holeId == hole1.EntityKey );           
            var hasHole2   = mappedModel.HoleIds.Any( holeId => holeId == hole2.EntityKey );
            var holeCount  = mappedModel.HoleIds.Count();

            Assert.IsTrue( hasHole1 );
            Assert.IsTrue( hasHole2 );

            Assert.AreEqual( expectedHoleCount, holeCount );

            // Test hold pars in detail
            // - correct [order:par] combos -- should be [1:3] and [4:2]
            // - correct count
            var hole1Par   = mappedModel.HolePars[1];
            var hole2Par   = mappedModel.HolePars[2];
            var holeCount2 = mappedModel.HolePars.Count();

            Assert.AreEqual( 3, hole1Par                   );
            Assert.AreEqual( 4, hole2Par                   );
            Assert.AreEqual( expectedHoleCount, holeCount2 );
        }
    }
}
