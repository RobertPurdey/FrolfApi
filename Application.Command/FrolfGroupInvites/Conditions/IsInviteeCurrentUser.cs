using Domain.Commands;
using Domain.Entities;

namespace Application.Command.FrolfGroupInvites.Conditions
{
    /// <summary>
    /// Determines if the current logged in user id matches the user being invited.
    /// </summary>
    public class IsInviteeCurrentUser : Condition<FrolfGroupInvite>
    {
        public override bool Validate(FrolfGroupInvite entity)
        {
            return UserExtensions.GetCurrentUserId() == entity.InviteeId;
        }
    }
}
