using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;

namespace Application.Query.Services.FrolfGroups
{
    public class FrolfGroupInviteQueryService : QueryService<FrolfGroupInvite>
    {
        public FrolfGroupInviteQueryService(IEntityDbContext context)
            : base(context)
        {

        }
    }
}
