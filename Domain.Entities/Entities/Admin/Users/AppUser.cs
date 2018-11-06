using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Domain.Entities
{
    public class AppUser : IAppUser
    {
        public AppUser()
        {
            Players       = new HashSet<Player>();
            GroupInvites  = new HashSet<FrolfGroupInvite>();
        }

        public Guid EntityKey { get; set; }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Handle { get; set; }

        public string AuthenticationType { get; set; }
        public string Name => EntityKey.ToString();
        public IIdentity Identity => this;
        public bool IsAuthenticated => true;
        public bool IsInRole(string role) => false;

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<FrolfGroupInvite> GroupInvites { get; set; }
        public virtual ICollection<FrolfGroupInvite> SentInvites { get; set; }

        public virtual void SetAsCurrentUser()
        {
            UserExtensions.SetCurrentUser(this);
        }

        public virtual bool IsCurrentUser()
        { 
            return UserExtensions.GetCurrentUserId() == EntityKey;
        }
    }
}
