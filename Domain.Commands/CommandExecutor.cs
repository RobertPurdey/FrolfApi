using Domain.Commands.Contracts;

namespace Domain.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly ICommandLocator commandLocator;

        public CommandExecutor(ICommandLocator commandLocator)
        {
            this.commandLocator = commandLocator;
        }

        public void Execute<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            CallPreHandler(command, commandLocator.GetCommandPreHandler<TCommand>());
            CallHandler(command, commandLocator.GetCommandHandler<TCommand>());
        }

        private static void CallPreHandler<TCommand>(TCommand command, ICommandPreHandler<TCommand> handler)
            where TCommand : class, ICommand
        {
            if (handler != null)
            { 
                handler.PreHandle(command);
            }
        }

        private static void CallHandler<TCommand>(TCommand command, ICommandHandler<TCommand> handler)
            where TCommand : class, ICommand
        {
            if (handler != null)
            { 
                handler.Handle(command);
            }
        }
    }
}
