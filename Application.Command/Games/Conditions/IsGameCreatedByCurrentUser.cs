using Domain.Commands;
using Domain.Entities;

namespace Application.Command.Games.Conditions
{
    public class IsGameCreatedByCurrentUser : Condition<Game>
    {
        public IsGameCreatedByCurrentUser()
        {

        }

        public override bool Validate(Game entity)
        {
            return entity           != null
                && entity.CreatedBy == UserExtensions.GetCurrentUserId();
        }
    }
}
