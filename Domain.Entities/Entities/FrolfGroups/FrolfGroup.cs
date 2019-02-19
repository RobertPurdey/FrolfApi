using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class FrolfGroup : IGuidEntity, IOwnable
    {
        public FrolfGroup()
        {
            Members  = new HashSet<Player>();
            Invites  = new HashSet<FrolfGroupInvite>();
            Games    = new HashSet<Game>();
        }

        public Guid EntityKey { get; set; }
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<Player> Members { get; set; }
        public virtual ICollection<FrolfGroupInvite> Invites { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
