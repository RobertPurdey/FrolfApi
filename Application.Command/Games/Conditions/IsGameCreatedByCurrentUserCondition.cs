using Domain.Commands;
using Domain.Entities;

namespace Application.Command.Games.Conditions
{
    public class IsGameCreatedByCurrentUserCondition : Condition<Game>
    {
        public IsGameCreatedByCurrentUserCondition()
        {

        }

        public override bool Validate(Game entity)
        {
            return entity != null
                && entity.CreatedBy == UserExtensions.GetCurrentUserId();
        }
    }
}
