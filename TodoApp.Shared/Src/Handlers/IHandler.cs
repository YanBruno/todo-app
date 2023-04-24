using TodoApp.Shared.Src.Commands;

namespace TodoApp.Shared.Src.Handlers;

public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> HandleAsync(T command);
}
