using Domain.Commands;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.FrolfGroupInvites.Conditions
{
    /// <summary>
    /// Determines if the invite being processed is currently in the Pending state.
    /// </summary>
    public class IsInvitePendingCondition : Condition<FrolfGroupInvite>
    {
        private readonly IQueryService<FrolfGroupInvite> frolfGroupInviteService;

        public IsInvitePendingCondition(IQueryService<FrolfGroupInvite> frolfGroupInviteQueryService)
        {
            frolfGroupInviteService = frolfGroupInviteQueryService;
        }

        public override bool Validate(FrolfGroupInvite entity)
        {
            return frolfGroupInviteService
                .GetAll()
                .Any(g => g.EntityKey == entity.EntityKey 
                       && g.Status == InviteState.Pending);
        }
    }
}
