using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Query;

namespace Application.Query.Services.Users
{
    public class AppUserRefreshTokenQueryService : QueryService<AppUserRefreshToken>
    {
        public AppUserRefreshTokenQueryService(IEntityDbContext context)
            : base(context)
        {

        }
    }
}
