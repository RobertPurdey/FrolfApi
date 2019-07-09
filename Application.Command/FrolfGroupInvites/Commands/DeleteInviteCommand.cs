using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using System;

namespace Application.Command.FrolfGroupInvites.Commands
{
    public class DeleteInviteCommand : ICommand
    {
        public FrolfGroupInvite Invite { get; private set; }

        public DeleteInviteCommand(FrolfGroupInvite invite)
        {
            if ( invite            == null ) throw new ArgumentNullException("invite");
            if ( invite.Invitee    == null ) throw new ArgumentNullException("invitee");
            if ( invite.Inviter    == null ) throw new ArgumentNullException("inviter");
            if ( invite.FrolfGroup == null ) throw new ArgumentNullException("frolfgroup");

            Invite = invite;
        }
    }

    public class DeleteInviteCommandValidation : CommandPreHandler<DeleteInviteCommand>
    { 
        public DeleteInviteCommandValidation(IWorkUnit workUnit)
            : base (workUnit)
        {

        }

        public override void OnPreHandleCommand(DeleteInviteCommand command)
        {
            Assert(
                new IsInviteeCurrentUser().Validate(command.Invite),
                "Users can only accept their own invites.");
        }
    }

    public class DeleteInviteCommandHandler : CommandHandler<DeleteInviteCommand>
    {
        public DeleteInviteCommandHandler(IWorkUnit workUnit)
            : base (workUnit)
        {

        }

        protected override void OnHandleCommand(DeleteInviteCommand command)
        {
            GetRepository<FrolfGroupInvite>().Remove(command.Invite);
        }
    }
}
