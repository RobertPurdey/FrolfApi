using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.AppUsers.Conditions
{
    public class LoginNameExistsCondition : Condition<AppUser>
    {
        private readonly IQueryService<AppUser> appUserService;

        public LoginNameExistsCondition(IQueryService<AppUser> appUserQueryService)
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
