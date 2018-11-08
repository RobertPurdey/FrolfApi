using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System;
using System.Linq;

namespace Application.Command.FrolfGroupInvites.Conditions
{
    /// <summary>
    /// Determines if the user inviting has a high enough role to do so.
    /// </summary>
    public class CanInviterInviteCondition : Condition<FrolfGroupInvite>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupService;

        public CanInviterInviteCondition(IQueryService<FrolfGroup> frolfGroupQueryService)
        {
            frolfGroupService  = frolfGroupQueryService;
        }

        public override bool Validate(FrolfGroupInvite entity)
        {
            var groupInvitedTo = frolfGroupService
                .GetAll()
                .SingleOrDefault(g => g.EntityKey == entity.FrolfGroupId);

            if ( groupInvitedTo == null ) return false;

            return IsInviterGroupAdmin(entity.Inviter.EntityKey, groupInvitedTo);
        }

        private bool IsInviterGroupAdmin(Guid inviterId, FrolfGroup group)
        {
            return group.Members.Any(
                p => p.AppUserId == inviterId
                  && p.GroupRole == GroupRole.Administrator);
        }
    }
}
