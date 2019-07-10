using Application.Command.Games.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Entities.Games;
using Domain.Query.Contracts;

namespace Application.Command.Games.Commands
{
    public class CompleteGameCommand : ICommand
    {
        public Game Game { get; set; }
    }

    public class CompleteGameCommandValidation : CommandPreHandler<CompleteGameCommand>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public CompleteGameCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroup> frolfGroupQueryService)
            : base(workUnit)
        {
            frolfGroupQuery = frolfGroupQueryService;
        }

        public override void OnPreHandleCommand(CompleteGameCommand command)
        {
            AssertGameInProgress(command);
        }

        private void AssertGameInProgress(CompleteGameCommand command)
        {
            var isValid = new IsGameInProgress().Validate(command.Game);

            Assert(isValid, "Cannot complete a game that is not in the 'In Progress' state");
        }
    }

    public class CompleteGameCommandHandler : CommandHandler<CompleteGameCommand>
    {
        public CompleteGameCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(CompleteGameCommand command)
        {
            command.Game.State = GameState.Completed;

            GetRepository<Game>().Update(command.Game);
        }
    }
}
