using Domain.Commands.Contracts;

namespace Domain.Commands
{
    public abstract class CommandHandler<TCommand> : CommandHandlerBase<TCommand>, ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        protected CommandHandler(IWorkUnit workUnit) 
            : base(workUnit)
        {
        }

        public void Handle(TCommand command)
        {
            OnHandleCommand(command);
            WorkUnit.Commit();
        }

        protected abstract void OnHandleCommand(TCommand command);
    }
}
