using Domain.Commands;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{
    /// <summary>
    /// Determines if the user can invite to the frolf group.
    /// 
    /// Rules:
    ///     - Must be in the group
    ///     - Must be an administrator
    /// </summary>
    public class CanUserInvite : Condition<FrolfGroup>
    {
        private readonly Guid appuserId;

        public CanUserInvite(Guid appuserId)
        {
            this.appuserId = appuserId;
        }

        public override bool Validate(FrolfGroup entity)
        {
            return IsInviterGroupAdmin(entity);
        }

        private bool IsInviterGroupAdmin(FrolfGroup group)
        {
            return group.Members.Any(
                p => p.AppUserId == appuserId
                  && p.GroupRole == GroupRole.Administrator);
        }
    }
}
