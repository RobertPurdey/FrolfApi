using Domain.Entities;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.HoleScores
{
    public class HoleScoreQueryArg : GuidEntityQueryArg<HoleScore>
    {
        public Guid? GameId { get; set; }
        public int? HoleNumber { get; set; }

        protected override Expression<Func<HoleScore, bool>> ConstructFilter()
        {
            return ForGame()
                .And( ForHoleNumber() );
        }

        private Expression<Func<HoleScore, bool>> ForGame()
        {
            return GameId.HasValue
                ? h => h.Round.GameId == GameId
                : True;
        }

        private Expression<Func<HoleScore, bool>> ForHoleNumber()
        {
            return HoleNumber.HasValue
                ? h => h.Hole.Order == HoleNumber
                : True;
        }
    }
}
