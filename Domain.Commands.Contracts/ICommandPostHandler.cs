namespace Domain.Commands.Contracts
{
    public interface ICommandPostHandler<in TCommand>
        where TCommand : class, ICommand
    {
        void PostHandle(TCommand command);
    }
}
