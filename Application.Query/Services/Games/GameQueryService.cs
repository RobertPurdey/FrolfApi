using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Query.Services.Games
{
    public class GameQueryService : QueryService<Game>
    {
        public GameQueryService(IEntityDbContext context)
            : base(context)
        {

        }

        public override IQueryable<Game> GetAll()
        {
            var query         = base.GetAll();
            var currUserGuid  = UserExtensions.GetCurrentUserId();

            return query.Where(
                g => g.FrolfGroup.Members.Any( m => m.AppUserId == currUserGuid) );
        }

        public override IQueryable<Game> GetWithQueryArg(IQueryArg<Game> arg)
        {
            var query        = base.GetWithQueryArg(arg);
            var currUserGuid = UserExtensions.GetCurrentUserId();

            return query.Where(
                g => g.FrolfGroup.Members.Any(m => m.AppUserId == currUserGuid));
        }
    }
}
