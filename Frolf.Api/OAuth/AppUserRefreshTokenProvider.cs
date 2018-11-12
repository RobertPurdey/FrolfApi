using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Security.Contracts;
using Security.OAuth;

namespace Frolf.Api.OAuth
{
    public class AppUserRefreshTokenProvider : RefreshTokenProviderBase<AppUserRefreshToken>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ICommandLocator commandLocator;
        private IWorkUnit workUnit;

        public AppUserRefreshTokenProvider(
            ICommandExecutor executor,
            ICommandLocator locator,
            IEncryptionManager encrytor)
            : base (encrytor)
        {
            commandExecutor = executor;
            commandLocator  = locator;
        }

        protected override IRepository<AppUserRefreshToken> GetRefreshTokenRepository()
        {
            var workUnit = commandLocator.GetWorkUnit();

            return workUnit.GetRepository<AppUserRefreshToken>();
        }

        protected override void OnCreateToken(AppUserRefreshToken token)
        {
            commandExecutor.Execute(new AddAppUserRefreshTokenCommand
            {
                Token = token
            });
        }

        protected override void OnDeleteToken(AppUserRefreshToken token)
        {
            commandExecutor.Execute(new DeleteAppUserRefreshTokenCommand
            {
                Token = new AppUserRefreshToken() { Id = token.Id }
            });
        }
    }
}