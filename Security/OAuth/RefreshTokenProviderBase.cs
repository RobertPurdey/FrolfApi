using Domain.Commands.Contracts;
using Microsoft.Owin.Security.Infrastructure;
using Security.Contracts;
using System;
using System.Security.Claims;

namespace Security.OAuth
{
    public abstract class RefreshTokenProviderBase<TRefreshToken> : AuthenticationTokenProvider
        where TRefreshToken : class, IRefreshToken, new()
    {
        private readonly IEncryptionManager encryptor;

        protected RefreshTokenProviderBase(IEncryptionManager encryptor)
        {
            this.encryptor = encryptor;
        }

        public override void Create(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");
            var issuedOn       = DateTime.UtcNow;

            var token = new TRefreshToken
            {
                Token     = encryptor.Hash(refreshTokenId),
                UserId    = Guid.Parse(context.Ticket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value),
                IssuedOn  = issuedOn,
                ExpiresOn = issuedOn.AddHours(6)
            };

            context.Ticket.Properties.IssuedUtc  = token.IssuedOn;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresOn;
            token.SerializedTicket               = context.SerializeTicket();

            OnCreateToken(token);
            context.SetToken(refreshTokenId);
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            var hashedTokenId = encryptor.Hash(context.Token);
            var refreshToken  = GetRefreshTokenRepository().
                GetByKey(x => x.Token == hashedTokenId);

            if (refreshToken == null)
            {
                return;
            }

            context.DeserializeTicket(refreshToken.SerializedTicket);

            if (context.Ticket.Properties.ExpiresUtc < DateTime.UtcNow)
            {
                return;
            }

            OnDeleteToken(refreshToken);
        }
        protected abstract void OnCreateToken(TRefreshToken token);
        protected abstract void OnDeleteToken(TRefreshToken token);
        protected abstract IRepository<TRefreshToken> GetRefreshTokenRepository();
    }
}
