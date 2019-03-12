using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.Games.Conditions
{
    public class GameOnlyContainsGroupMembers : Condition<Game>
    {
        public GameOnlyContainsGroupMembers()
        {

        }

        public override bool Validate(Game entity)
        {
            var gamePlayerIds   = entity.Rounds.Select(r => r.Player.EntityKey);
            var groupMemberIds  = entity.FrolfGroup.Members.Select(fg => fg.EntityKey);

            return gamePlayerIds.Any( id => !groupMemberIds.Contains(id) );
        }
    }
}
