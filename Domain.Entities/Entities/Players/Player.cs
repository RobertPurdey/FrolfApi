using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Player : IGuidEntity
    {
        public Player()
        {
            FrolfGroups = new HashSet<FrolfGroup>();
        }

        public Guid EntityKey { get; set; }
        public Guid AppUserId { get; set; }
        public GroupRole GroupRole { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<FrolfGroup> FrolfGroups { get; set; }
    }
}
