using Domain.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.FrolfGroupInvites.Conditions
{
    public class InviteExistsCondition : Condition<FrolfGroupInvite>
    {
        public InviteExistsCondition()
        {

        }

        public override bool Validate(FrolfGroupInvite entity)
        {
            return entity != null;
        }
    }
}
