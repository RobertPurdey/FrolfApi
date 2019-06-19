using Domain.Commands;
using Domain.Entities;

namespace Application.Command.Players.Conditions
{
    /// <summary>
    /// Determines if the player is the current user making the api call.
    /// </summary>
    public class IsPlayerCurrentUser : Condition<Player>
    {
        public override bool Validate(Player entity)
        {
            return UserExtensions.GetCurrentUserId() == entity.AppUserId;
        }
    }
}
