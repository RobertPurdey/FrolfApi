using Domain.Entities.Contracts;
using System;
using System.Security.Principal;

namespace Domain.Entities
{
    public class AppUser : IAppUser
    {
        public Guid EntityKey { get; set; }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AuthenticationType { get; set; }

        public string Name => EntityKey.ToString();
        public IIdentity Identity => this;
        public bool IsAuthenticated => true;
        public bool IsInRole(string role) => false;

        public virtual void SetAsCurrentUser()
        {
            UserExtensions.SetCurrentUser(this);
        }

        public virtual bool IsCurrentUser()
        {
            var user = UserExtensions.GetCurrentUser();

            return user != null 
                && user.EntityKey == EntityKey;
        }
    }
}
