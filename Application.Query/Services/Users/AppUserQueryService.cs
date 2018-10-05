using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;

namespace Application.Query.Services.Users
{
    public class AppUserQueryService : QueryService<AppUser>
    {
        public AppUserQueryService(IEntityDbContext context)
            : base(context)
        {

        }
    }
}
