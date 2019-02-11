using Domain.Commands;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if the user inviting has a high enough role to do so.
    /// </summary>
    public class CanUserInviteCondition : Condition<FrolfGroup>
    {
        private readonly Guid inviterId;

        public CanUserInviteCondition(Guid inviterId)
        {
            this.inviterId = inviterId;
        }

        public override bool Validate(FrolfGroup entity)
        {
            return IsInviterGroupAdmin(entity);
        }

        private bool IsInviterGroupAdmin(FrolfGroup group)
        {
            return group.Members.Any(
                p => p.AppUserId == inviterId
                  && p.GroupRole == GroupRole.Administrator);
        }
    }
}
