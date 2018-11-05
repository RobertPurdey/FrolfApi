namespace Domain.Commands.Contracts
{
    public interface ICommandExecutor
    {
        void Execute<TCommand>(TCommand command)
            where TCommand : class, ICommand;
    }
}
