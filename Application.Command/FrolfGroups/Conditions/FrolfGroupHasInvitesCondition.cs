using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if the group has any invites
    /// </summary>
    public class FrolfGroupHasInvitesCondition : Condition<FrolfGroup>
    {
        public FrolfGroupHasInvitesCondition()
        {

        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity.Invites.Any();
        }
    }
}
