using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Composers.Games;
using Frolf.Api.Models.Games;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.Tests.Composers.Games
{
    [TestFixture]
    public class GameComposerTests
    {
        #region Setup

        GameCreationModel creationModel;
        GameComposer composer;

        Guid holeOneId;
        Hole holeOne;

        Guid holeTwoId;
        Hole holeTwo;

        Guid courseId;
        Course course;

        Guid playerOneId;
        Player playerOne;

        Guid playerTwoId;
        Player playerTwo;

        Guid frolfGroupId;
        FrolfGroup frolfGroup;

        List<FrolfGroup> frolfGroupDataStore;

        Mock<IQueryService<FrolfGroup>> mockFrolfGroupQuery;

        [SetUp]
        public void Setup()
        {
            SetupHoles();
            SetupCourse();
            SetupPlayers();

            SetupFrolfGroup();
            SetupFrolfGroupDataStore();

            SetupMockFrolfGroupQuery();

            creationModel = new GameCreationModel
            {
                Name      = "Whatsfrolf",
                CourseId  = courseId,
                GroupId   = frolfGroupId,
                PlayerIds = new List<Guid> { playerOneId, playerTwoId }
            };

            composer = new GameComposer(mockFrolfGroupQuery.Object);
        }

        private void SetupHoles()
        {
            holeOneId = Guid.NewGuid();
            holeOne   = new Hole
            {
                EntityKey = holeOneId
            };

            holeTwoId = Guid.NewGuid();
            holeTwo   = new Hole
            {
                EntityKey = holeTwoId
            };
        }

        private void SetupCourse()
        {
            courseId = Guid.NewGuid();

            course = new Course
            {
                EntityKey = courseId,
                Holes     = new List<Hole> { holeOne, holeTwo }
            };
        }

        private void SetupPlayers()
        {
            playerOneId = Guid.NewGuid();
            playerOne   = new Player
            {
                EntityKey = playerOneId
            };

            playerTwoId = Guid.NewGuid();
            playerTwo   = new Player
            { 
                EntityKey = playerTwoId
            };
        }

        private void SetupFrolfGroup()
        {
            frolfGroupId = Guid.NewGuid();
            frolfGroup   = new FrolfGroup
            {
                EntityKey = frolfGroupId,
                Courses   = new List<Course> { course },
                Members   = new List<Player> { playerOne, playerTwo }
            };
        }

        private void SetupFrolfGroupDataStore()
        {
            frolfGroupDataStore = new List<FrolfGroup> { frolfGroup };
        }

        private void SetupMockFrolfGroupQuery()
        {
            mockFrolfGroupQuery = new Mock<IQueryService<FrolfGroup>>();

            mockFrolfGroupQuery.Setup( m => m.GetAll() ).Returns( frolfGroupDataStore.AsQueryable );
        }

        #endregion

        [Test]
        public void GameComposer_Compose_ExpectedGameCreated()
        {
            var newGame = composer.NewGame(creationModel);

            Assert.AreEqual( newGame.Name,                  creationModel.Name     );
            Assert.AreEqual( newGame.CourseId,              creationModel.CourseId );
            Assert.AreEqual( newGame.Course.EntityKey,      creationModel.CourseId );
            Assert.AreEqual( newGame.FrolfGroupId,          creationModel.GroupId  );
            Assert.AreEqual( newGame.FrolfGroup.EntityKey,  creationModel.GroupId  );
        }

        [Test]
        public void GameComposer_Compose_ExpectedRounds()
        {
            var newGame            = composer.NewGame(creationModel);           
            var expectedRoundCount = 2;

            Assert.AreEqual( expectedRoundCount, newGame.Rounds.Count() );          

            // verify 1st player
            Assert.IsTrue(newGame.Rounds.Any(r => r.Player.EntityKey == playerOneId));

            var palyerOneRound = newGame.Rounds.Where(r => r.Player.EntityKey == playerOneId).Single();

            Assert.IsTrue( palyerOneRound.HoleScores.Any( h => h.Hole.EntityKey == holeOneId ) );
            Assert.IsTrue( palyerOneRound.HoleScores.Any( h => h.Hole.EntityKey == holeTwoId ) );

            // verify 2nd player
            Assert.IsTrue(newGame.Rounds.Any(r => r.Player.EntityKey == playerTwoId));

            var playerTwoRound = newGame.Rounds.Where(r => r.Player.EntityKey == playerTwoId).Single();

            Assert.IsTrue( playerTwoRound.HoleScores.Any( h => h.Hole.EntityKey == holeOneId ) );
            Assert.IsTrue( playerTwoRound.HoleScores.Any( h => h.Hole.EntityKey == holeTwoId ) );
        }

        [Test]
        public void GameComposer_Compose_ExpectException_WhenCreationModelNull()
        {
            Assert.Throws<ArgumentNullException>( () => composer.NewGame(null) );
        }

        [Test]
        public void GameComposer_Compose_ExpectException_WhenGroupDoesntExist()
        {
            creationModel.GroupId = Guid.NewGuid();

            Assert.Throws<Exception>( () => composer.NewGame(creationModel) );
        }

        [Test]
        public void GameComposer_Compose_ExpectException_WhenGroupCourseDoesntExist()
        {
            creationModel.CourseId = Guid.NewGuid();

            Assert.Throws<Exception>( () => composer.NewGame(creationModel) );
        }

        [Test]
        public void GameComposer_Compose_ExpectException_WhenNoPlayersFoundInGroup()
        {
            creationModel.PlayerIds = new List<Guid> { Guid.NewGuid() };

            Assert.Throws<Exception>( () => composer.NewGame(creationModel) );
        }
    }
}
