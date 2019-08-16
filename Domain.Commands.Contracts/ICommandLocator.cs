using System.Collections.Generic;

namespace Domain.Commands.Contracts
{
    public interface ICommandLocator
    {
        ICommandPreHandler<TCommand> GetCommandPreHandler<TCommand>()
            where TCommand : class, ICommand;

        ICommandHandler<TCommand> GetCommandHandler<TCommand>() 
            where TCommand : class, ICommand;

        IWorkUnit GetWorkUnit();
    }
}
