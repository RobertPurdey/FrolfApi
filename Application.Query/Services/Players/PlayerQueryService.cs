using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;

namespace Application.Query.Services.Players
{
    public class PlayerQueryService : QueryService<Player>
    {
        public PlayerQueryService(IEntityDbContext context)
            : base(context)
        {

        }
    }
}
