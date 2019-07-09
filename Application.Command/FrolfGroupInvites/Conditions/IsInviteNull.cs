using Domain.Commands;
using Domain.Entities;

namespace Application.Command.FrolfGroupInvites.Conditions
{
    public class IsInviteNull : Condition<FrolfGroupInvite>
    {
        public override bool Validate(FrolfGroupInvite entity)
        {
            return entity == null;
        }
    }
}
