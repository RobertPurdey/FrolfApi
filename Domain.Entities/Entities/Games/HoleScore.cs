using Domain.Entities.Contracts;
using System;

namespace Domain.Entities
{
    public class HoleScore : IGuidEntity
    {
        public Guid EntityKey { get; set; }
        public Guid HoleId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid RoundId { get; set; }
        public Guid GameId { get; set; }

        public int Strokes { get; set; }

        public virtual Hole Hole { get; set; }
        public virtual Player Player { get; set; }
        public virtual Round Round { get; set; }
        public virtual Game Game { get; set; }
    }
}
