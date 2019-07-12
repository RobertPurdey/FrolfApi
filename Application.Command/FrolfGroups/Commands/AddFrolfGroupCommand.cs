using Application.Command.FrolfGroups.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;

namespace Application.Command.FrolfGroups.Commands
{
    public class AddFrolfGroupCommand : ICommand
    {
        public FrolfGroup newEntity { get; set; }
    }

    public class AddFrolfGroupCommandValidation : CommandPreHandler<AddFrolfGroupCommand>
    {
        public AddFrolfGroupCommandValidation(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public override void OnPreHandleCommand(AddFrolfGroupCommand command)
        {
            // todo: move this to entity validator?
            Assert( !string.IsNullOrWhiteSpace(command.newEntity.Name), "Group name must be set");
            AssertHasNoInvites(command);
            AssertOnlyMemberIsCurrentUserAsAdmin(command);
        }

        private void AssertHasNoInvites(AddFrolfGroupCommand command)
        {
            var isValid = !new DoesFrolfGroupHaveInvites().Validate(command.newEntity);

            Assert(isValid, "Cannot add a new frolf group if it contains invites");
        }

        private void AssertOnlyMemberIsCurrentUserAsAdmin(AddFrolfGroupCommand command)
        {
            var isValid = new DoesFrolfGroupOnlyContainCurrentUser().Validate(command.newEntity);

            Assert(isValid, "The player creating the group must be in the group being created and be the only player added.");
        }
    }

    public class AddFrolfGroupCommandHandler : CommandHandler<AddFrolfGroupCommand>
    {
        public AddFrolfGroupCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(AddFrolfGroupCommand command)
        {
            GetRepository<FrolfGroup>().Add(command.newEntity);
        }
    }
}
