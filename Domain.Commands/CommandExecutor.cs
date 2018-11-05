using Domain.Commands.Contracts;
using System.Collections.Generic;

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
            CallPreHandlers(command, commandLocator.GetCommandPreHandler<TCommand>());
            CallHandlers(command, commandLocator.GetCommandHandler<TCommand>());
            CallPostHandlers(command, commandLocator.GetCommandPostHandler<TCommand>());
        }

        private static void CallPreHandlers<TCommand>(TCommand command, IEnumerable<ICommandPreHandler<TCommand>> handlers)
            where TCommand : class, ICommand
        {
            foreach (var handler in handlers)
            {
                handler.PreHandle(command);
            }
        }

        private static void CallHandlers<TCommand>(TCommand command, IEnumerable<ICommandHandler<TCommand>> handlers)
            where TCommand : class, ICommand
        {
            foreach (var handler in handlers)
            {
                handler.Handle(command);
            }
        }

        private static void CallPostHandlers<TCommand>(TCommand command, IEnumerable<ICommandPostHandler<TCommand>> handlers)
            where TCommand : class, ICommand
        {
            foreach (var handler in handlers)
            {
                handler.PostHandle(command);
            }
        }
    }
}
