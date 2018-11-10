using Domain.Entities;
using Domain.Query;
using System;
using System.Linq.Expressions;

namespace Application.Query.Services.FrolfGroups
{
    public class FrolfGroupInviteQueryArg : GuidEntityQueryArg<FrolfGroupInvite>
    {
        public Guid CurrentUserGuid { get; set; }

        protected override Expression<Func<FrolfGroupInvite, bool>> ConstructFilter()
        {
            return ForCurrentUser()
                ;
        }

        private Expression<Func<FrolfGroupInvite, bool>> ForCurrentUser()
        {
            return inv => inv.InviteeId == CurrentUserGuid;
        }
    }
}
