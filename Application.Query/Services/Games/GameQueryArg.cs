using Domain.Entities;
using Domain.Entities.Entities.Games;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.Games
{
    public class GameQueryArg : GuidEntityQueryArg<Game>
    {
        public GameState? State { get; set; }

        protected override Expression<Func<Game, bool>> ConstructFilter()
        {
            return HasState();
        }

        private Expression<Func<Game, bool>> HasState()
        {
            return State.HasValue
                ? e => e.State == State
                : True;
        }
    }
}
