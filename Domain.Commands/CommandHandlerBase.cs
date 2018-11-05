using Domain.Commands.Contracts;
using System;

namespace Domain.Commands
{
    public class CommandHandlerBase<TCommand>
        where TCommand : ICommand
    {
        protected IWorkUnit WorkUnit { get; }

        protected CommandHandlerBase(IWorkUnit workUnit)
        {
            WorkUnit = workUnit ?? throw new ArgumentNullException(nameof(workUnit));
        }

        protected virtual IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return WorkUnit.GetRepository<TEntity>();
        }               
    }
}
