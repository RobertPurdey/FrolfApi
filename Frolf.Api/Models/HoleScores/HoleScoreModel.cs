using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.HoleScores
{
    public class HoleScoreModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid HoleId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid RoundId { get; set; }
        public int Strokes { get; set; }
        public int Score { get; set; }
        public int HolePar { get; set; }
        public string PlayerHandle { get; set; }
    }

    public class HoleScoreFilterModel : IFilterModel
    {
        public Guid? GameId { get; set; }
        public int? HoleNumber { get; set; }
    }
}