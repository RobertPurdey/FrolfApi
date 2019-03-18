using Domain.Entities;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.Games
{
    public class GameQueryArg : GuidEntityQueryArg<Game>
    {
        protected override Expression<Func<Game, bool>> ConstructFilter()
        {
            return base.ConstructFilter();
        }
    }
}
