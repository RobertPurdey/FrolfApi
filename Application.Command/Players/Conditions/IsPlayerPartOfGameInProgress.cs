using Domain.Commands;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using System.Linq;

namespace Application.Command.Players.Conditions
{
    /// <summary>
    /// Determines if a player is part of a game in progress.
    /// </summary>
    public class IsPlayerPartOfGameInProgress : Condition<Player>
    {
        public override bool Validate(Player entity)
        {
            return entity.Rounds.Any(r => r.Game.State == GameState.InProgress);
        }
    }
}
