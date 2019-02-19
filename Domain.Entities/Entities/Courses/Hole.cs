using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Hole : IGuidEntity
    {
        public Hole()
        {
            HoleScores = new HashSet<HoleScore>();
        }

        public Guid EntityKey { get; set; }
        public Guid CourseId { get; set; }
        public int Order { get; set; }
        public int Par { get; set; }

        public virtual Course Course { get; set; }
        
        public virtual ICollection<HoleScore> HoleScores { get; set; }
    }
}
