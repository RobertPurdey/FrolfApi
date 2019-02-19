using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Round : IGuidEntity
    {
        public Round()
        {
            HoleScores = new HashSet<HoleScore>();
        }

        public Guid EntityKey { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GameId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }

        public virtual ICollection<HoleScore> HoleScores { get; set; }
    }
}
