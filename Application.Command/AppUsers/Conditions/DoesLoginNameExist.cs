using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.AppUsers.Conditions
{
    public class DoesLoginNameExist : Condition<AppUser>
    {
        private readonly IQueryService<AppUser> appUserService;

        public DoesLoginNameExist(IQueryService<AppUser> appUserQueryService)
        {
            appUserService = appUserQueryService;
        }

        public override bool Validate(AppUser entity)
        {
            return appUserService
                .GetAll()
                .Any(g => g.LoginName == entity.LoginName);
        }
    }
}
