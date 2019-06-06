using Domain.Entities.Contracts;
using Domain.Entities.Entities.Games;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Game : IGuidEntity, IOwnable
    {
        public Guid EntityKey { get; set; }
        public Guid CourseId { get; set; }
        public Guid FrolfGroupId { get; set; }
        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public GameState State { get; set; }

        public virtual Course Course { get; set; }
        public virtual FrolfGroup FrolfGroup { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }
        public virtual ICollection<HoleScore> HoleScores { get; set; }
    }
}
