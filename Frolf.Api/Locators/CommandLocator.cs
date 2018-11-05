using Domain.Commands.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Frolf.Api.Locators
{
    public class CommandLocator : ICommandLocator
    {
        private readonly IDependencyScope dependencyContainer;

        public CommandLocator(IDependencyScope container)
        {
            dependencyContainer = container;
        }

        public IEnumerable<ICommandPreHandler<TCommand>> GetCommandPreHandler<TCommand>() 
            where TCommand : class, ICommand
        {
            return dependencyContainer
                .GetServices(typeof(ICommandPreHandler<TCommand>))
                .Cast<ICommandPreHandler<TCommand>>();
        }

        public IEnumerable<ICommandHandler<TCommand>> GetCommandHandler<TCommand>() 
            where TCommand : class, ICommand
        {
            return dependencyContainer
                .GetServices(typeof(ICommandHandler<TCommand>))
                .Cast<ICommandHandler<TCommand>>();
        }

        public IEnumerable<ICommandPostHandler<TCommand>> GetCommandPostHandler<TCommand>() 
            where TCommand : class, ICommand
        {
            return dependencyContainer
                .GetServices(typeof(ICommandPostHandler<TCommand>))
                .Cast<ICommandPostHandler<TCommand>>();
        }

        public IEnumerable<IEntityValidator<TEntity>> GetEntityValidators<TEntity>()
            where TEntity : class
        {
            return dependencyContainer
                .GetServices(typeof(IEntityValidator<TEntity>))
                .Cast<IEntityValidator<TEntity>>();
        }
    }
}