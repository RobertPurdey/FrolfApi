using Application.Command.Games.Conditions;
using Application.Command.HoleScores.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.HoleScores
{
    public class BatchUpdateHoleScoreCommand : ICommand
    {
        public Guid GameId;
        public IEnumerable<HoleScore> HolesToUpdate { get; set; }

        public BatchUpdateHoleScoreCommand(Guid gameId, IEnumerable<HoleScore> holesToUpdate)
        {
            GameId          = gameId;
            HolesToUpdate   = holesToUpdate;
        }
    }

    public class BatchUpdateHoleScoreCommandValidation : CommandPreHandler<BatchUpdateHoleScoreCommand>
    {
        private readonly IQueryService<Game> gameQueryService;

        public BatchUpdateHoleScoreCommandValidation(
            IWorkUnit workUnit,
            IQueryService<Game> gameQueryService)
            : base(workUnit)
        {
            this.gameQueryService = gameQueryService;
        }

        public override void OnPreHandleCommand(BatchUpdateHoleScoreCommand command)
        {
            // Only need one match to continue validation
            Assert(command.HolesToUpdate.Any(hs => hs.GameId == command.GameId), "game id doesn't match hole score game id");

            AssertOnlyOneGamesHolesUpdated(command);
            AssertCurrentUserIsGameOwner(command);
        }

        private void AssertOnlyOneGamesHolesUpdated(BatchUpdateHoleScoreCommand command)
        {
            var isOnlyOneGame = new AreHoleScoresForSameGameCondition().Validate(command.HolesToUpdate);

            Assert(
                isOnlyOneGame,
                "Cannot update hole scores for different games at the same time.");
        }

        private void AssertCurrentUserIsGameOwner(BatchUpdateHoleScoreCommand command)
        {
            var game = gameQueryService.GetAll()
                .Where(g => g.EntityKey == command.GameId)
                .FirstOrDefault();

            var isGameOwnerCurrentUser = new IsGameCreatedByCurrentUserCondition().Validate(game);

            Assert(isGameOwnerCurrentUser, "Only creator can score a game");
        }
    }

    public class BatchUpdateHoleScoreCommandHandler : CommandHandler<BatchUpdateHoleScoreCommand>
    {
        public BatchUpdateHoleScoreCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(BatchUpdateHoleScoreCommand command)
        {
            var holeScoreRepo = GetRepository<HoleScore>();

            foreach (var holeScore in command.HolesToUpdate)
            {
                holeScoreRepo.Update(holeScore);
            }
        }
    }
}
