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
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public RemovePlayerCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroup> frolfGroupQuery)
            : base(workUnit)
        {
            this.frolfGroupQuery = frolfGroupQuery;
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
            var isValid = new IsUserAGroupMemberCondition(command.Player.AppUserId).Validate(command.FrolfGroup);

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
        private readonly IQueryService<HoleScore> holeScoreQuery;
        private readonly IQueryService<Round> roundQuery;

        public RemovePlayerCommandHandler(
            IWorkUnit workUnit,
            IQueryService<HoleScore> holeScoreQuery,
            IQueryService<Round> roundQuery)
            : base(workUnit)
        {
            this.holeScoreQuery = holeScoreQuery;
            this.roundQuery     = roundQuery;
        }

        protected override void OnHandleCommand(RemovePlayerCommand command)
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
        private void RemoveHoleScores(RemovePlayerCommand command)
        {
            var holeScoreToRemove = holeScoreQuery
                .GetAll()
                .Where(
                    hs => hs.Game.FrolfGroupId  == command.FrolfGroup.EntityKey
                       && hs.PlayerId           == command.Player.EntityKey);

            var holeScoreRepo = GetRepository<HoleScore>();

            foreach (var holeScore in holeScoreToRemove)
            {
                holeScoreRepo.Remove(holeScore);
            }
        }

        private void RemoveRounds(RemovePlayerCommand command)
        {
            var roundsToRemove = roundQuery
                .GetAll()
                .Where(
                    r => r.Game.FrolfGroupId == command.FrolfGroup.EntityKey
                      && r.PlayerId          == command.Player.EntityKey);
        }
    }
}
