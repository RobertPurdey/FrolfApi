using Domain.Commands;
using Domain.Entities;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if the current user is the group creator
    /// </summary>
    public class IsCurrentUserGroupCreator : Condition<FrolfGroup>
    {
        public IsCurrentUserGroupCreator()
        {

        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity.CreatedBy == UserExtensions.GetCurrentUserId();
        }
    }
}
