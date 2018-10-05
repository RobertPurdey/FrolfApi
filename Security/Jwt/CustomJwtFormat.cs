using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Security.Jwt
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            //string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            //if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            //Audience audience = AudiencesStore.FindAudience(audienceId);

            //string symmetricKeyAsBase64 = audience.Base64Secret;
            //var keyProvider  = new SecurityKeyProvider();
            var keyByteArray       = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");
            var securityKey        = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var issued  = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(
                _issuer,
                "self",
                data.Identity.Claims,
                issued.Value.UtcDateTime,
                expires.Value.UtcDateTime,
                signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
