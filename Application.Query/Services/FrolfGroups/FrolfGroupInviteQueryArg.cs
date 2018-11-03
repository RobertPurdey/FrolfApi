using Domain.Entities;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.FrolfGroups
{
    public class FrolfGroupInviteQueryArg : GuidEntityQueryArg<FrolfGroupInvite>
    {
        public Guid CurrentUserGuid { get; set; }
        public InviteState? InviteStatus { get; set; }

        protected override Expression<Func<FrolfGroupInvite, bool>> ConstructFilter()
        {
            return ForCurrentUser()
                .And(HasInviteState())
                ;
        }

        private Expression<Func<FrolfGroupInvite, bool>> ForCurrentUser()
        {
            return inv => inv.AppUserId == CurrentUserGuid;
        }

        private Expression<Func<FrolfGroupInvite, bool>> HasInviteState()
        {
            return InviteStatus.HasValue 
                ? inv => inv.status == InviteStatus
                : True;
        }
    }
}
