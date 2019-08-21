using System.Collections.Generic;

namespace Frolf.Api.Models.Games
{
    public class PlayerGameResultModel
    {
        public string PlayerName { get; set; }
        public int TotalStrokes { get; set; }
        public int TotalScore { get; set; }
        public int Rank { get; set; }
        public IDictionary<int, int> Scores { get; set; }
        public IDictionary<int, int> Strokes { get; set; }
    }
}