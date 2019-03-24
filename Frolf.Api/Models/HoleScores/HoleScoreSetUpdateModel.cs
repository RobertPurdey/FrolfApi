using System;
using System.Collections.Generic;

namespace Frolf.Api.Models.HoleScores
{
    public class HoleScoreSetUpdateModel
    {
        public Guid GameId { get; set; }
        public IDictionary<Guid, int> HoleScoreUpdates { get; set; }
    }
}