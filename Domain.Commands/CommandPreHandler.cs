using Domain.Commands.Contracts;
using System;

namespace Domain.Commands
{
    public abstract class CommandPreHandler<TCommand> : CommandHandlerBase<TCommand>, ICommandPreHandler<TCommand>
        where TCommand : class, ICommand
    {
        protected CommandPreHandler(IWorkUnit workUnit)
            : base(workUnit)
        {

        }
 
        public abstract void OnPreHandleCommand(TCommand command);

        public void PreHandle(TCommand command)
        {
            OnPreHandleCommand(command);
        }

        protected void Assert(bool condition, string message)
        {
            if ( !condition )
            {
                throw new Exception(message);
            }
        }
    }
}
