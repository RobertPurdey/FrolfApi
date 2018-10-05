using System.Security.Principal;

namespace Domain.Entities.Contracts
{
    /// <summary>
    /// Defines an application user.
    /// </summary>
    public interface IAppUser : IGuidEntity, IIdentity, IPrincipal
    {
        string LoginName { get; set; }
        string Password { get; set; }
        string Email { get; set; }

        // todo: possibly app identification info
    }
}
