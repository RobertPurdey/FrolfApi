using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.Composers.Games
{
    public interface IGameComposer
    {
        Game NewGame(GameCreationModel creationModel);
    }

    public class GameComposer : IGameComposer
    {
        private readonly IQueryService<Course> courseQuerySerivce;
        private readonly IQueryService<FrolfGroup> groupQueryService;

        public GameComposer(
            IQueryService<Course> courseQuerySerivce,
            IQueryService<FrolfGroup> groupQueryService)
        {
            this.courseQuerySerivce = courseQuerySerivce;
            this.groupQueryService  = groupQueryService;
        }

        public Game NewGame(GameCreationModel creationModel)
        {
            if ( creationModel == null ) throw new ArgumentNullException(creationModel.ToString());

            var course = GetCourse( creationModel.CourseId );
            if ( course == null ) throw new Exception("Unable to retrieve course.");

            var group = GetFrolfGroup(creationModel.GroupId);
            if ( group  == null ) throw new Exception("Unable to retrieve group.");

            var players = GetMembers(group, creationModel.PlayerIds);
            if (players.Count() == 0) throw new Exception("Unable to retrieve players.");

            return NewGame(creationModel, course, group, players);
        }

        private Game NewGame(
            GameCreationModel creationModel,
            Course course,
            FrolfGroup group,
            IEnumerable<Player> players)
        {
            var newGame = new Game
            {
                Name        = creationModel.Name,
                Course      = course,
                FrolfGroup  = group,
                Rounds      = new List<Round>()
            };

            foreach ( var player in players )
            {
                newGame.Rounds.Add( CreateRound(player, course) );
            }

            return newGame;
        }

        private Round CreateRound(Player player, Course course)
        {
            var newRound = new Round
            {
                EntityKey   = Guid.NewGuid(),
                Player      = player,
                HoleScores  = new List<HoleScore>()
            };

            foreach ( var hole in course.Holes )
            {
                newRound.HoleScores.Add(new HoleScore
                {
                    EntityKey  = Guid.NewGuid(),
                    Hole       = hole,
                    Player     = player,
                    Score      = hole.Par
                });
            }

            return newRound;
        }

        private Course GetCourse(Guid courseId)
        {
            return courseQuerySerivce.GetAll()
                .Where(c => c.EntityKey == courseId)
                .SingleOrDefault();
        }

        private FrolfGroup GetFrolfGroup(Guid groupId)
        {
            return groupQueryService.GetAll()
                .Where(c => c.EntityKey == groupId)
                .SingleOrDefault();
        }

        private IEnumerable<Player> GetMembers(FrolfGroup group, IEnumerable<Guid> playerIds)
        {
            var distinctIds = new HashSet<Guid>(playerIds.Distinct());

            return group.Members.Where(
                m => distinctIds.Contains(m.EntityKey) );
        }
    }
}