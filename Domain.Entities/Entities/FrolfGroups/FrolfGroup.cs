using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class FrolfGroup : IGuidEntity, IOwnable
    {
        public Guid EntityKey { get; set; }
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<Player> GroupMembers { get; set; }
    }
}
