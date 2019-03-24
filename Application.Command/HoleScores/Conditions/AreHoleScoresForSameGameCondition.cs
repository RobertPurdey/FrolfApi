using Domain.Commands;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.HoleScores.Conditions
{
    public class AreHoleScoresForSameGameCondition : Condition<IEnumerable<HoleScore>>
    {
        public AreHoleScoresForSameGameCondition()
        {

        }

        public override bool Validate(IEnumerable<HoleScore> holeScores)
        {
            return holeScores.Select(hs => hs.GameId).Distinct().Count() == 1;
        }
    }
}
