using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if there is a single group admin who is the current user
    /// </summary>
    public class DoesFrolfGroupOnlyContainCurrentUser : Condition<FrolfGroup>
    {
        public DoesFrolfGroupOnlyContainCurrentUser()
        {

        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity.Members.Count             == 1
                && entity.Members.First().AppUserId == UserExtensions.GetCurrentUserId()
                && entity.Members.First().GroupRole == GroupRole.Administrator;
        }
    }
}
