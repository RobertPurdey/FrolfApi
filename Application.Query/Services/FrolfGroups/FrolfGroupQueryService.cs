using System.Linq;
using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;

namespace Application.Query.Services.FrolfGroups
{
    public class FrolfGroupQueryService : QueryService<FrolfGroup>
    {
        public FrolfGroupQueryService(IEntityDbContext context)
            : base(context)
        {

        }

        public override IQueryable<FrolfGroup> GetAll()
        {
            var query        = base.GetAll();
            var currUserGuid = UserExtensions.GetCurrentUserId();

            return query.Where(
                g => g.Members.Any(m => m.AppUserId == currUserGuid) );
        }
    }
}
