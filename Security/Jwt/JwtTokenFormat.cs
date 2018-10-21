using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Security.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Security.Jwt
{
    public class JwtTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly ISecurityKeyProvider keyProvider;

        public JwtTokenFormat(ISecurityKeyProvider keyProvider)
        {
            this.keyProvider = keyProvider;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var signature   = GetSignature();
            var issuedDate  = data.Properties.IssuedUtc.Value.UtcDateTime;
            var expiryDate  = data.Properties.ExpiresUtc.Value.UtcDateTime;

            var token = new JwtSecurityToken(
                null,
                null,
                data.Identity.Claims,
                issuedDate,
                expiryDate,
                signature);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {

            var claimsPrincipal =
                new JwtSecurityTokenHandler().ValidateToken(
                    protectedText,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey  = true,
                        IssuerSigningKey          = keyProvider.GetSigningKey()
                    },
                    out SecurityToken securityToken);

            var identity = claimsPrincipal.Identities.ElementAt(0);

            return new AuthenticationTicket(
                identity,
                new AuthenticationProperties());
        }

        private SigningCredentials GetSignature()
        {
            return new SigningCredentials(
                keyProvider.GetSigningKey(),
                SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
