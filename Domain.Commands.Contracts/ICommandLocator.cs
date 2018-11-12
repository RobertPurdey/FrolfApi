using System.Collections.Generic;

namespace Domain.Commands.Contracts
{
    public interface ICommandLocator
    {
        IEnumerable<ICommandPreHandler<TCommand>> GetCommandPreHandler<TCommand>()
            where TCommand : class, ICommand;

        IEnumerable<ICommandHandler<TCommand>> GetCommandHandler<TCommand>() 
            where TCommand : class, ICommand;

        IEnumerable<ICommandPostHandler<TCommand>> GetCommandPostHandler<TCommand>()
            where TCommand : class, ICommand;

        IEnumerable<IEntityValidator<TEntity>> GetEntityValidators<TEntity>()
            where TEntity : class;

        IWorkUnit GetWorkUnit();
    }
}
