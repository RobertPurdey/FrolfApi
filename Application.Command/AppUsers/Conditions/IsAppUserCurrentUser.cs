using Domain.Commands;
using Domain.Entities;

namespace Application.Command.AppUsers.Conditions
{
    public class IsAppUserCurrentUser : Condition<AppUser>
    {
        public override bool Validate(AppUser entity)
        {
            return UserExtensions.GetCurrentUserId() == entity.EntityKey;
        }
    }
}
