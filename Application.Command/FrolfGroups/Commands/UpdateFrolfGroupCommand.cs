using Application.Command.FrolfGroups.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;

namespace Application.Command.FrolfGroups.Commands
{
    public class UpdateFrolfGroupCommand : ICommand
    {
        public FrolfGroup entity { get; set; }
    }

    public class UpdateFrolfGroupCommandValidation : CommandPreHandler<UpdateFrolfGroupCommand>
    {
        public UpdateFrolfGroupCommandValidation(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public override void OnPreHandleCommand(UpdateFrolfGroupCommand command)
        {
            // todo: move this to entity validator?
            Assert(!string.IsNullOrWhiteSpace(command.entity.Name), "Group name must be set");
            AssertUserIsCreator(command);
        }

        private void AssertUserIsCreator(UpdateFrolfGroupCommand command)
        {
            var isValid = new IsCurrentUserGroupCreator().Validate(command.entity);

            Assert(isValid, "Cannot modify the group if not the creator of the group");
        }
    }

    public class UpdateFrolfGroupCommandHandler : CommandHandler<UpdateFrolfGroupCommand>
    {
        public UpdateFrolfGroupCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(UpdateFrolfGroupCommand command)
        {
            GetRepository<FrolfGroup>().Update(command.entity);
        }
    }
}
