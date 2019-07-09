using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.AppUsers.Conditions
{
    public class DoesFriendCodeExist : Condition<AppUser>
    {
        private readonly IQueryService<AppUser> appUserService;

        public DoesFriendCodeExist(IQueryService<AppUser> appUserQueryService)
        {
            appUserService = appUserQueryService;
        }

        public override bool Validate(AppUser entity)
        {
            return appUserService
                .GetAll()
                .Any(g => g.FriendCode == entity.FriendCode);
        }
    }
}
