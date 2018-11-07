using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.AppUsers.Conditions
{
    // todo: remove if not needed

    /// <summary>
    /// Determines if the App User exists
    /// </summary>
    public class AppUserExistsCondition : Condition<AppUser>
    {
        private readonly IQueryService<AppUser> appUserService;

        public AppUserExistsCondition(IQueryService<AppUser> appUserQueryService)
        {
            appUserService = appUserQueryService;
        }

        public override bool Validate(AppUser entity)
        {
            return appUserService
                .GetAll()
                .Any(g => g.EntityKey == entity.EntityKey);
        }
    }
}
