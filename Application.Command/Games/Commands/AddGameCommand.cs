using Application.Command.FrolfGroups.Conditions;
using Application.Command.Games.Conditions;
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;

namespace Application.Command.Games.Commands
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
            AssertCurrentUserInGroup(command);
            AssertAllPlayersAreGroupMembers(command);
        }

        private void AssertCurrentUserInGroup(AddGameCommand command)
        {
            var isValid = new IsUserAGroupMember( UserExtensions.GetCurrentUserId() )
                .Validate( command.NewGame.FrolfGroup );

            Assert(isValid, "Cannot create a game for a group you are not a part of.");
        }

        private void AssertAllPlayersAreGroupMembers(AddGameCommand command)
        {
            var isValid = new IsGameMembersOnly().Validate(command.NewGame);

            Assert(isValid, "Cannot create a game when some members are not in the group.");
        }

        private void AssertCourseIsInGroup(AddGameCommand command)
        {
            var isValid = new IsGameCourseInGroup().Validate(command.NewGame);

            Assert(isValid, "Game must use a course that is attached to the group the game is for.");
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
