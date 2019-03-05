using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;

namespace Application.Command.Games
{
    public class AddGameCommand : ICommand
    {
        public Game NewGame { get; set; }
    }

    public class AddGameCommandValidation : CommandPreHandler<AddGameCommand>
    {
        private readonly IQueryService<FrolfGroup> frolfGroupQuery;

        public AddGameCommandValidation(
            IWorkUnit workUnit,
            IQueryService<FrolfGroup> frolfGroupQueryService)
            : base(workUnit)
        {
            frolfGroupQuery = frolfGroupQueryService;
        }

        public override void OnPreHandleCommand(AddGameCommand command)
        {
            // Asserts
            // - Current user is in the group
            // - All players are in the group
        }
    }

    public class AddGameCommandHandler : CommandHandler<AddGameCommand>
    {
        public AddGameCommandHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        protected override void OnHandleCommand(AddGameCommand command)
        {
            GetRepository<Game>().Add(command.NewGame);
        }
    }
}
