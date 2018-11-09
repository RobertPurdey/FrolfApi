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

    public class DeclineInviteCommandValidation : CommandPreHandler<AcceptInviteCommand>
    { 
        private readonly IQueryService<FrolfGroupInvite> groupInviteService;

        public DeclineInviteCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroupInvite> groupQueryInviteService)
            : base (workUnit)
        {
            groupInviteService  = groupQueryInviteService;
        }

        public override void OnPreHandleCommand(AcceptInviteCommand command)
        {
            Assert(
                new IsInviteeCurrentUserCondition().Validate(command.Invite),
                "Users can only accept their own invites.");

            Assert(
                new IsInvitePendingCondition(groupInviteService).Validate(command.Invite),
                "Invite has already been processed.");
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
            command.Invite.Status = InviteState.Declined;

            GetRepository<FrolfGroupInvite>().Update(command.Invite);
        }
    }
}
