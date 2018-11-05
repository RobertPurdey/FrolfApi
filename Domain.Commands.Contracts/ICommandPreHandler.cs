namespace Domain.Commands.Contracts
{
    public interface ICommandPreHandler<in TCommand>
        where TCommand : class, ICommand
    {
        void PreHandle(TCommand command);
    }
}
