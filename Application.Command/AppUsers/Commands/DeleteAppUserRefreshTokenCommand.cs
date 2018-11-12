using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;

namespace Application.Command.AppUsers.Commands
{
    public class DeleteAppUserRefreshTokenCommand : ICommand
    {
        public AppUserRefreshToken Token { get; set; }
    }

    public class DeleteAppUserRefreshTokenCommandHandler : CommandHandler<DeleteAppUserRefreshTokenCommand>
    {
        public DeleteAppUserRefreshTokenCommandHandler(IWorkUnit workUnit) 
            : base (workUnit)
        {

        }

        protected override void OnHandleCommand(DeleteAppUserRefreshTokenCommand command)
        {
            // Because this command is used in the OAuth pipeline by the AppUserRefreshTokenProvider,
            // the passed in entity needs to be retrieved from within the current nested container.
            // Otherwise, removing the entity from the db context will throw an error.
            var repository = GetRepository<AppUserRefreshToken>();
            var token      = repository.GetByKey(t => t.Id == command.Token.Id);

            repository.Remove(token);
        }
    }
}
