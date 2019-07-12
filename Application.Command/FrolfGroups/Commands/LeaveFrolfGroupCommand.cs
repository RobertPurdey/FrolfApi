using Application.Command.FrolfGroups.Conditions;
using Application.Command.Players.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;

namespace Application.Command.FrolfGroups.Commands
{

    public class LeaveFrolfGroupCommand : ICommand
    {
        public Player Player;
        public FrolfGroup FrolfGroup;
    }

    public class LeaveFrolfGroupCommandValidation : CommandPreHandler<LeaveFrolfGroupCommand>
    {
        public LeaveFrolfGroupCommandValidation(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public override void OnPreHandleCommand(LeaveFrolfGroupCommand command)
        {
            AssertPlayerIsCurrentUser(command);
            AssertPlayerIsInGroup(command);
            AssertPlayerIsNotGroupCreator(command);
            AssertPlayerIsNotPartOfGameInProgress(command);
        }

        private void AssertPlayerIsCurrentUser(LeaveFrolfGroupCommand command)
        {
            var isValid = new IsPlayerCurrentUser().Validate(command.Player);

            Assert(isValid, "Player cannot leave the group if its not the user making the call.");
        }

        private void AssertPlayerIsInGroup(LeaveFrolfGroupCommand command)
        {
            var isValid = new IsUserAGroupMember(command.Player.AppUserId).Validate(command.FrolfGroup);

            Assert(isValid, "Player cannot leave the group if its not in the group.");
        }

        private void AssertPlayerIsNotGroupCreator(LeaveFrolfGroupCommand command)
        {
            var isValid = !new IsPlayerCreatorOfGroup().Validate(command.Player);

            Assert(isValid, "Player cannot leave the group when they are the creator of the group");
        }

        private void AssertPlayerIsNotPartOfGameInProgress(LeaveFrolfGroupCommand command)
        {
            var isValid = !new IsPlayerPartOfGameInProgress().Validate(command.Player);

            Assert(isValid, "Player cannot be part of a game in progress");
        }
    }

    public class LeaveFrolfGroupCommandHandler : CommandHandler<LeaveFrolfGroupCommand>
    {
        public LeaveFrolfGroupCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(LeaveFrolfGroupCommand command)
        {
            GetRepository<Player>().Remove(command.Player);
        }
    }
}
