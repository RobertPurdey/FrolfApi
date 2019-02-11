using Application.Command.FrolfGroupInvites.Conditions;
using Application.Command.FrolfGroups.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System;
using System.Linq;

namespace Application.Command.FrolfGroupInvites.Commands
{
    public class AddInviteCommand : ICommand
    {
        public FrolfGroupInvite Invite { get; private set; }

        public AddInviteCommand(FrolfGroupInvite invite)
        {
            if (invite == null) throw new ArgumentNullException("invite");

            Invite = invite;
        }
    }

    public class AddInviteCommandValidation : CommandPreHandler<AddInviteCommand>
    {
        private readonly IQueryService<FrolfGroupInvite> frolfGroupInviteQuery;
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public AddInviteCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroupInvite> frolfGroupInviteQueryService,
            IQueryService<FrolfGroup> frolfGroupQueryService)
            : base(workUnit)
        {
            frolfGroupInviteQuery   = frolfGroupInviteQueryService;
            frolfGroupQuery         = frolfGroupQueryService;
        }

        public override void OnPreHandleCommand(AddInviteCommand command)
        {
            AssertInviterIsCurrentUser(command.Invite);
            AssertInviteIsntSent(command.Invite);

            var frolfGroup = frolfGroupQuery
                .GetAll()
                .Where(fg => fg.EntityKey == command.Invite.FrolfGroupId)
                .SingleOrDefault();

            AssertFrolfGroupExists(frolfGroup);
            AssertInviterHasPrivileges(frolfGroup, command.Invite.InviterId);
            AssertInviteeIsntInGroup(frolfGroup, command.Invite.InviteeId);
        }

        private void AssertInviterIsCurrentUser(FrolfGroupInvite invite)
        {
            var isValid = new IsInviterCurrentUserCondition().Validate(invite);

            Assert(isValid, "The inviter must be the current user.");
        }

        private void AssertInviterHasPrivileges(FrolfGroup frolfGroup, Guid inviterId)
        {
            var isValid = new CanUserInviteCondition(inviterId).Validate(frolfGroup);

            Assert(isValid, "Inviter must be an administrator in the group being invited to.");
        }

        private void AssertInviteeIsntInGroup(FrolfGroup frolfGroup, Guid InviteeId)
        {
            var isValid = new IsUserAGroupMemberCondition(InviteeId).Validate(frolfGroup);

            Assert(isValid, "User being invited is already in the group.");

        }

        private void AssertFrolfGroupExists(FrolfGroup frolfGroup)
        {
            var isValid = new FrolfGroupExistsCondition().Validate(frolfGroup);

            Assert(isValid, "Frolf group being invited to does not exist.");
        }

        private void AssertInviteIsntSent(FrolfGroupInvite invite)
        {
            var frolfGroupInvite = frolfGroupInviteQuery
                .GetAll()
                .Where(fg => fg.FrolfGroupId == invite.FrolfGroupId
                         &&  fg.InviteeId    == invite.InviteeId)
                .SingleOrDefault();

            var isValid = new InviteExistsCondition().Validate(frolfGroupInvite);

            Assert(isValid, "Invite has already been sent to the user.");
        }
    }

    public class AddInviteCommandHandler : CommandHandler<AddInviteCommand>
    {
        public AddInviteCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(AddInviteCommand command)
        {
            GetRepository<FrolfGroupInvite>().Add(command.Invite);
        }
    }
}
