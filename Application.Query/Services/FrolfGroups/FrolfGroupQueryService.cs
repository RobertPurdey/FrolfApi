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
    }
}
