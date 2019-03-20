using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;
using System.Linq;

namespace Application.Query.Services.HoleScores
{
    public class HoleScoreQueryService : QueryService<HoleScore>
    {
        public HoleScoreQueryService(IEntityDbContext context)
            : base(context)
        {

        }

        public override IQueryable<HoleScore> GetAll()
        {
            var query         = base.GetAll();
            var currUserGuid  = UserExtensions.GetCurrentUserId();

            // Only allow access to hole scores where the current user is part
            // of the group attached to the game the score is for.
            return query.Where(
                g => g.Round.Game.FrolfGroup.Members.Any(p => p.AppUserId == currUserGuid));
        }
    }
}
