using Application.Command.FrolfGroups.Conditions;
using Application.Command.Players.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.Players.Commands
{
    public class RemovePlayerCommand : ICommand
    {
        public Player Player;
        public FrolfGroup FrolfGroup;
    }

    public class RemovePlayerCommandValidation : CommandPreHandler<RemovePlayerCommand>
    {
        public RemovePlayerCommandValidation(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public override void OnPreHandleCommand(RemovePlayerCommand command)
        {
            AssertCurrentUserIsGroupCreator(command);
            AssertPlayerIsInGroup(command);
            AssertPlayerIsNotGroupCreator(command);
            AssertPlayerIsNotPartOfGameInProgress(command);
        }

        private void AssertCurrentUserIsGroupCreator(RemovePlayerCommand command)
        {
            var isValid = new IsCurrentUserGroupCreator().Validate(command.FrolfGroup);

            Assert(isValid, "Only the group creator can remove players.");
        }

        private void AssertPlayerIsInGroup(RemovePlayerCommand command)
        {
            var isValid = new IsUserAGroupMember(command.Player.AppUserId).Validate(command.FrolfGroup);

            Assert(isValid, "Player cannot be removed if they are not in the group.");
        }

        private void AssertPlayerIsNotGroupCreator(RemovePlayerCommand command)
        {
            var isValid = !new IsPlayerCreatorOfGroup().Validate(command.Player);

            Assert(isValid, "Group creator cannot be removed");
        }

        private void AssertPlayerIsNotPartOfGameInProgress(RemovePlayerCommand command)
        {
            var isValid = !new IsPlayerPartOfGameInProgress().Validate(command.Player);

            Assert(isValid, "Player cannot be part of a game in progress");
        }
    }

    public class RemovePlayerCommandHandler : CommandHandler<RemovePlayerCommand>
    {
        public RemovePlayerCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(RemovePlayerCommand command)
        {
            GetRepository<Player>().Remove(command.Player);
        }
    }
}
