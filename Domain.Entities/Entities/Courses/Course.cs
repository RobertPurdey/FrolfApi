using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Course : IGuidEntity
    {
        public Course()
        {
            Holes  = new HashSet<Hole>();
            Games  = new HashSet<Game>();
        }

        public Guid EntityKey { get; set; }
        public Guid FrolfGroupId { get; set; }
        public string Name { get; set; }

        public virtual FrolfGroup FrolfGroup { get; set; }

        public virtual ICollection<Hole> Holes { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
