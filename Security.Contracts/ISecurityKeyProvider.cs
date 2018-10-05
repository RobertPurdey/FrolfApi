using Microsoft.IdentityModel.Tokens;

namespace Security.Contracts
{
    public interface ISecurityKeyProvider
    {
        SecurityKey GetSigningKey();
    }
}
