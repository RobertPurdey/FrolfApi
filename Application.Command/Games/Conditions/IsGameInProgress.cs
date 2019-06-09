using Domain.Commands;
using Domain.Entities;
using Domain.Entities.Entities.Games;

namespace Application.Command.Games.Conditions
{
    public class IsGameInProgress : Condition<Game>
    {
        public IsGameInProgress()
        {

        }

        public override bool Validate(Game entity)
        {
            return entity.State == GameState.InProgress;
        }
    }
}
