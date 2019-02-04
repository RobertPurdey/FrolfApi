using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if there is a single player who is the current user
    /// </summary>
    class FrolfGroupOnlyContainsCurrentUser : Condition<FrolfGroup>
    {
        public FrolfGroupOnlyContainsCurrentUser()
        {

        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity.Members.Count == 1
                && entity.Members.First().AppUserId == UserExtensions.GetCurrentUserId();
        }
    }
}
