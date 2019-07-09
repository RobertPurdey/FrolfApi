using Application.Command.FrolfGroups.Conditions;
using Application.Command.Players.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System.Linq;

namespace Application.Command.FrolfGroups.Commands
{

    public class LeaveFrolfGroupCommand : ICommand
    {
        public Player Player;
        public FrolfGroup FrolfGroup;
    }

    public class LeaveFrolfGroupCommandValidation : CommandPreHandler<LeaveFrolfGroupCommand>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public LeaveFrolfGroupCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroup> frolfGroupQuery)
            : base(workUnit)
        {
            this.frolfGroupQuery = frolfGroupQuery;
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
        private readonly IQueryService<HoleScore> holeScoreQuery;
        private readonly IQueryService<Round> roundQuery;

        public LeaveFrolfGroupCommandHandler(
            IWorkUnit workUnit,
            IQueryService<HoleScore> holeScoreQuery,
            IQueryService<Round> roundQuery)
            : base(workUnit)
        {
            this.holeScoreQuery = holeScoreQuery;
            this.roundQuery     = roundQuery;
        }

        protected override void OnHandleCommand(LeaveFrolfGroupCommand command)
        {
            // Remove all associations the player has with the frolf group before removing the player
            RemoveHoleScores(command);
            RemoveRounds(command);

            GetRepository<Player>().Remove(command.Player);

        }

        /// <summary>
        /// Remove hole scores associated with the player for the group the playe is being removed from.
        /// </summary>
        /// <param name="command"></param>
        private void RemoveHoleScores(LeaveFrolfGroupCommand command)
        {
            var holeScoreToRemove = holeScoreQuery
                .GetAll()
                .Where(
                    hs => hs.Game.FrolfGroupId == command.FrolfGroup.EntityKey
                       && hs.PlayerId          == command.Player.EntityKey);

            var holeScoreRepo = GetRepository<HoleScore>();

            foreach (var holeScore in holeScoreToRemove)
            {
                holeScoreRepo.Remove(holeScore);
            }
        }

        private void RemoveRounds(LeaveFrolfGroupCommand command)
        {
            var roundsToRemove = roundQuery
                .GetAll()
                .Where(
                    r => r.Game.FrolfGroupId == command.FrolfGroup.EntityKey
                      && r.PlayerId          == command.Player.EntityKey);
        }
    }
}
