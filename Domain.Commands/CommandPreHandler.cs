using Domain.Commands.Contracts;

namespace Domain.Commands
{
    public abstract class CommandPreHandler<TCommand> : CommandHandlerBase<TCommand>, ICommandPreHandler<TCommand>
        where TCommand : class, ICommand
    {
        protected CommandPreHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }

        public void PreHandle(TCommand command)
        {
            OnPreHandleCommand(command);
        }

        public abstract void OnPreHandleCommand(TCommand command);
    }
}
