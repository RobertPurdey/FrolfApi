using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using System;

namespace Application.Command.FrolfGroupInvites.Commands
{
    public class AcceptInviteCommand : ICommand
    {
        public FrolfGroupInvite Invite { get; private set; }

        public AcceptInviteCommand(FrolfGroupInvite invite)
        {
            if ( invite            == null ) throw new ArgumentNullException("invite");
            if ( invite.Invitee    == null ) throw new ArgumentNullException("invitee");
            if ( invite.Inviter    == null ) throw new ArgumentNullException("inviter");
            if ( invite.FrolfGroup == null ) throw new ArgumentNullException("frolfgroup");

            Invite = invite;
        }
    }

    public class AcceptInviteCommandValidation : CommandPreHandler<AcceptInviteCommand>
    { 
        public AcceptInviteCommandValidation(IWorkUnit workUnit)
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

    public class AcceptInviteCommandHandler : CommandHandler<AcceptInviteCommand>
    {
        public AcceptInviteCommandHandler(IWorkUnit workUnit)
            : base (workUnit)
        {

        }

        protected override void OnHandleCommand(AcceptInviteCommand command)
        {
            var newPlayer = new Player
            {
                AppUserId    = command.Invite.InviteeId,
                FrolfGroupId = command.Invite.FrolfGroupId,
                Handle       = command.Invite.Invitee.Handle,
                GroupRole    = GroupRole.Member,
                AppUser      = command.Invite.Invitee,
                FrolfGroup   = command.Invite.FrolfGroup
            };

            GetRepository<Player>().Add(newPlayer);
            GetRepository<FrolfGroupInvite>().Remove(command.Invite);
        }
    }
}
