using Application.Command.AppUsers.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.AppUsers.Commands
{
    public class UpdateAppUserCommand : ICommand
    {
        public AppUser User { get; set; }
    }

    public class UpdateAppUserCommandValidation : CommandPreHandler<UpdateAppUserCommand>
    {
        public UpdateAppUserCommandValidation(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public override void OnPreHandleCommand(UpdateAppUserCommand command)
        {
            AssertIsUpdatingSelf(command.User);
        }

        private void AssertIsUpdatingSelf(AppUser user)
        {
            var isValid = new IsAppUserCurrentUser().Validate(user);

            // Vague reason for failure
            Assert(isValid, "Cannot update this account");
        }
    }

    public class UpdateAppUserCommandHandler : CommandHandler<UpdateAppUserCommand>
    {
        private readonly IQueryService<Player> playerQuery;

        public UpdateAppUserCommandHandler(
            IWorkUnit workUnit,
            IQueryService<Player> playerQueryService)
            : base(workUnit)
        {
            playerQuery = playerQueryService;
        }

        protected override void OnHandleCommand(UpdateAppUserCommand command)
        {
            UpdateOwnPlayerHandles(command.User);
            GetRepository<AppUser>().Update(command.User);
        }

        private void UpdateOwnPlayerHandles(AppUser user)
        {
            var playerRepo  = GetRepository<Player>();
            var players     = playerQuery.GetAll().Where(e => e.AppUserId == user.EntityKey);

            foreach (var player in players)
            {
                player.Handle = user.Handle;
                playerRepo.Update(player);
            }
        }
    }
}
