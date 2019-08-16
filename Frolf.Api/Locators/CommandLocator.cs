using Domain.Commands.Contracts;
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

        public ICommandPreHandler<TCommand> GetCommandPreHandler<TCommand>() 
            where TCommand : class, ICommand
        {
            return (ICommandPreHandler<TCommand>)
                dependencyContainer.GetService(typeof(ICommandPreHandler<TCommand>));
        }

        public ICommandHandler<TCommand> GetCommandHandler<TCommand>() 
            where TCommand : class, ICommand
        {
            return (ICommandHandler<TCommand>) 
                dependencyContainer.GetService(typeof(ICommandHandler<TCommand>));
        }

        // todo: find out if this is still needed
        public IWorkUnit GetWorkUnit()
        {
            return (IWorkUnit) dependencyContainer.GetService(typeof(IWorkUnit));
        }
    }
}