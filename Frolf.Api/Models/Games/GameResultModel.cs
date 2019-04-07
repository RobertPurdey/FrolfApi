using System.Collections.Generic;

namespace Frolf.Api.Models.Games
{
    public class GameResultModel
    {
        public string CourseName { get; set; } 
        public int CoursePar { get; set; }
        public int HoleCount { get; set; }
        public IDictionary<int, int> HolePars { get; set; }
        public IEnumerable<PlayerGameResultModel> PlayerResults { get; set; }
    }
}