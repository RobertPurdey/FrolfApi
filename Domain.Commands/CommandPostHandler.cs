using Domain.Commands.Contracts;

namespace Domain.Commands
{
    public abstract class CommandPostHandler<TCommand> : CommandHandlerBase<TCommand>, ICommandPostHandler<TCommand>
        where TCommand : class, ICommand
    {
        protected CommandPostHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public void PostHandle(TCommand command)
        {
            OnPostHandleCommand(command);
        }

        public abstract void OnPostHandleCommand(TCommand command);
    }
}
