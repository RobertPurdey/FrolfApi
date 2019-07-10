using Domain.Commands;
using Domain.Entities;
using System.Linq;

namespace Application.Command.Games.Conditions
{
    public class IsGameMembersOnly : Condition<Game>
    {
        public IsGameMembersOnly()
        {

        }

        public override bool Validate(Game entity)
        {
            var gamePlayerIds   = entity.Rounds.Select(r => r.Player.EntityKey);
            var groupMemberIds  = entity.FrolfGroup.Members.Select(fg => fg.EntityKey);

            var nonMembers = gamePlayerIds.Except(groupMemberIds);

            return !nonMembers.Any();
        }
    }
}
