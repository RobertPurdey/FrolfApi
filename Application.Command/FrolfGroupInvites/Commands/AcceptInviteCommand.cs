using Application.Command.FrolfGroupInvites.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System;

namespace Application.Command.FrolfGroupInvites.Commands
{
    public class AcceptInviteCommand : ICommand
    {
        public FrolfGroupInvite Invite { get; private set; }

        public AcceptInviteCommand(FrolfGroupInvite invite)
        {
            if ( invite            == null ) throw new ArgumentNullException("invite");
            if ( invite.Invitee    == null ) throw new ArgumentNullException("invite");
            if ( invite.Inviter    == null ) throw new ArgumentNullException("invite");
            if ( invite.FrolfGroup == null ) throw new ArgumentNullException("invite");

            Invite = invite;
        }
    }

    public class AcceptInviteCommandValidation : CommandPreHandler<AcceptInviteCommand>
    { 
        private readonly IQueryService<FrolfGroupInvite> groupInviteService;
        private readonly IQueryService<FrolfGroup> groupService;
        private readonly IQueryService<AppUser> appUserService;

        public AcceptInviteCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroupInvite> groupQueryInviteService,
            IQueryService<FrolfGroup> groupQueryService,
            IQueryService<AppUser> appUserQueryService)
            : base (workUnit)
        {
            groupInviteService  = groupQueryInviteService;
            groupService        = groupQueryService;
            appUserService      = appUserQueryService;
        }

        public override void OnPreHandleCommand(AcceptInviteCommand command)
        {
            Assert(
                new IsInviteeCurrentUserCondition().Validate(command.Invite),
                "Users can only accept their own invites.");

            Assert(
                new IsInvitePendingCondition(groupInviteService).Validate(command.Invite),
                "Invite has already been processed.");

            Assert(
                new CanInviterInviteCondition(groupService).Validate(command.Invite),
                "Inviter's group role is too low to invite.");
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

            command.Invite.Status = InviteState.Accepted;

            GetRepository<Player>().Add(newPlayer);
            GetRepository<FrolfGroupInvite>().Update(command.Invite);
        }
    }
}
