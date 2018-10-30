using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Player : IGuidEntity
    {
        public Player()
        {

        }

        public Guid EntityKey { get; set; }
        public Guid AppUserId { get; set; }
        public Guid FrolfGroupId { get; set; }
        public GroupRole GroupRole { get; set; }
        public string Handle { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual FrolfGroup FrolfGroup { get; set; }
    }
}
