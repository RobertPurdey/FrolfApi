using Domain.Commands;
using Domain.Entities;

namespace Application.Command.Players.Conditions
{
    /// <summary>
    /// Determines if the player is the creator of the group
    /// </summary>
    public class IsPlayerCreatorOfGroup : Condition<Player>
    {
        public override bool Validate(Player entity)
        {
            return entity.AppUserId == entity.FrolfGroup.CreatedBy;
        }
    }
}
