using Domain.Entities;
using Frolf.Api.Mappers.HoleScores;
using Frolf.Api.Models.HoleScores;
using NUnit.Framework;
using System;

namespace Frolf.Api.Tests.Mappers.HoleScoreScores
{
    [TestFixture]
    public class HoleScoreScoreMapperTests
    {
        #region Setup
        
        HoleScoreMapper mapper;

        HoleScore entity;
        HoleScoreModel model;

        Hole hole;

        Player player;

        int expectedScore;

        [SetUp]
        public void Setup()
        {
            SetupHole();
            SetupPlayer();

            SetupEntity();
            SetupModel();

            mapper = new HoleScoreMapper();

            expectedScore = -2;
        }

        private void SetupHole()
        {
            hole = new Hole
            {
                EntityKey = Guid.NewGuid(),
                Par       = 3
            };
        }

        private void SetupPlayer()
        {
            player = new Player
            {
                EntityKey = Guid.NewGuid(),
                Handle    = "Stimpy"
            };
        }

        private void SetupEntity()
        {          
            entity = new HoleScore
            {
                EntityKey = Guid.NewGuid(),
                HoleId    = Guid.NewGuid(),
                PlayerId  = Guid.NewGuid(),
                RoundId   = Guid.NewGuid(),
                Strokes   = 1,
                Hole      = hole,
                Player    = player
            };
        }

        private void SetupModel()
        {
            model = new HoleScoreModel
            {
                IdKey  = Guid.NewGuid(),
            };
        }

        #endregion

        [Test]
        public void HoleScoreMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new HoleScore();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,  mappedEntity.EntityKey );
        }

        [Test]
        public void HoleScoreMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new HoleScoreModel();

            mapper.MapToApiModel(mappedModel, entity);

            Assert.AreEqual( entity.EntityKey,  mappedModel.IdKey    );
            Assert.AreEqual( entity.HoleId,     mappedModel.HoleId   );
            Assert.AreEqual( entity.PlayerId,   mappedModel.PlayerId );
            Assert.AreEqual( entity.RoundId,    mappedModel.RoundId  );
            Assert.AreEqual( entity.Strokes,    mappedModel.Strokes  );
            Assert.AreEqual( expectedScore,     mappedModel.Score    );

            Assert.AreEqual(entity.Player.Handle, mappedModel.PlayerHandle );
            Assert.AreEqual(entity.Hole.Par,      mappedModel.HolePar      );
        }
    }
}
