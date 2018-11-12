using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;

namespace Application.Command.AppUsers.Commands
{
    public class AddAppUserRefreshTokenCommand : ICommand
    {
        public AppUserRefreshToken Token { get; set; }
    }

    public class AddAppUserRefreshTokenCommandHandler : CommandHandler<AddAppUserRefreshTokenCommand>
    {
        public AddAppUserRefreshTokenCommandHandler(IWorkUnit workUnit) 
            : base (workUnit)
        {

        }

        protected override void OnHandleCommand(AddAppUserRefreshTokenCommand command)
        {
            GetRepository<AppUserRefreshToken>().Add(command.Token);
        }
    }
}
