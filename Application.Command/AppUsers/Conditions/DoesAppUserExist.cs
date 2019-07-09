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
    public class DoesAppUserExist : Condition<AppUser>
    {
        private readonly IQueryService<AppUser> appUserService;

        public DoesAppUserExist(IQueryService<AppUser> appUserQueryService)
        {
            appUserService = appUserQueryService;
        }

        public override bool Validate(AppUser entity)
        {
            var s = appUserService
                .GetAll();
                return s.Any(g => g.EntityKey == entity.EntityKey);
        }
    }
}
