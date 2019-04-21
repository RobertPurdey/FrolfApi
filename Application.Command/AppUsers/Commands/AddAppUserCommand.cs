using Application.Command.AppUsers.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;

namespace Application.Command.AppUsers.Commands
{
    public class AddAppUserCommand : ICommand
    {
        public AppUser NewUser { get; set; }
    }

    public class AddAppUserCommandValidation : CommandPreHandler<AddAppUserCommand>
    {
        private readonly IQueryService<AppUser> appUserQuery;

        public AddAppUserCommandValidation(
            IWorkUnit workUnit,
            IQueryService<AppUser> appUserQueryService)
            : base(workUnit)
        {
            appUserQuery = appUserQueryService;
        }

        public override void OnPreHandleCommand(AddAppUserCommand command)
        {
            AssertFriendCodeUnique(command);
            AssertLoginNameUnique(command);
        }

        private void AssertFriendCodeUnique(AddAppUserCommand command)
        {
            var isValid = !new FriendCodeExistsCondition(appUserQuery).Validate(command.NewUser);

            // Vague reason for failure to conceal giving away sensative information
            Assert(isValid, "Cannot create user");
        }

        private void AssertLoginNameUnique(AddAppUserCommand command)
        {
            var isValid = !new LoginNameExistsCondition(appUserQuery).Validate(command.NewUser);

            // Vague for failure to conceal giving away sensative information
            Assert(isValid, "Login name exists.");
        }
    }

    public class AddAppUserCommandHandler : CommandHandler<AddAppUserCommand>
    {
        public AddAppUserCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(AddAppUserCommand command)
        {
            GetRepository<AppUser>().Add(command.NewUser);
        }
    }
}
