using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.FrolfGroupInvites.Commands
{
    public class DeclineInviteCommand : ICommand
    {
        public FrolfGroupInvite Invite { get; private set; }

        public DeclineInviteCommand(FrolfGroupInvite invite)
        {
            if ( invite            == null ) throw new ArgumentNullException("invite");
            if ( invite.Invitee    == null ) throw new ArgumentNullException("invitee");
            if ( invite.Inviter    == null ) throw new ArgumentNullException("inviter");
            if ( invite.FrolfGroup == null ) throw new ArgumentNullException("frolfgroup");

            Invite = invite;
        }
    }

    public class DeleteInviteCommand : CommandPreHandler<AcceptInviteCommand>
    { 
        public DeleteInviteCommand(IWorkUnit workUnit)
            : base (workUnit)
        {

        }

        public override void OnPreHandleCommand(AcceptInviteCommand command)
        {
            Assert(
                new IsInviteeCurrentUserCondition().Validate(command.Invite),
                "Users can only accept their own invites.");
        }
    }

    public class DeclineInviteCommandHandler : CommandHandler<AcceptInviteCommand>
    {
        public DeclineInviteCommandHandler(IWorkUnit workUnit)
            : base (workUnit)
        {

        }

        protected override void OnHandleCommand(AcceptInviteCommand command)
        {
            GetRepository<FrolfGroupInvite>().Remove(command.Invite);
        }
    }
}
