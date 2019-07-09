using Domain.Commands;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Command.FrolfGroups.Conditions
{
    public class IsUserAGroupMember : Condition<FrolfGroup>
    {
        private readonly Guid userId;

        public IsUserAGroupMember(Guid userId)
        {
            this.userId = userId;
        }

        public override bool Validate(FrolfGroup entity)
        {
            return entity.Members.Any(m => m.AppUserId == userId);
        }
    }
}
