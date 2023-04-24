using Flunt.Notifications;
using TodoApp.Core.Src.Commands.TodoListCommands;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Repositories;
using TodoApp.Core.Src.Utils;
using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Commands;
using TodoApp.Shared.Src.Handlers;

namespace TodoApp.Core.Src.Handlers;

public class TodoListHandler
    : Notifiable<Notification>
    , IHandler<CreateTodoListCommand>
    , IHandler<DeleteTodoListCommand>

{
    private readonly IUserRepository _userRepository;
    private readonly ITodoListRepository _todoListRepository;

    public TodoListHandler(IUserRepository userRepository, ITodoListRepository todoListRepository)
    {
        _userRepository = userRepository;
        _todoListRepository = todoListRepository;
    }
    public async Task<ICommandResult> HandleAsync(CreateTodoListCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command)) return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _userRepository.GetAsync(command.UserId) is var user && user == null)
            return new GenericCommandResult(false, "Usuário inválido", null);

        var title = new Title(command.Title);
        var description = new Description(command.Description);

        var todoList = new TodoList(title, description, user);
        user.AddTodoList(todoList);

        AddNotifications(title, description, todoList, user);

        if (!IsValid)
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        await _todoListRepository.CreateAsync(todoList);

        return new GenericCommandResult(true, "Lista criada com sucesso", user!.Lists);
    }
    public async Task<ICommandResult> HandleAsync(DeleteTodoListCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command))
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if(await _todoListRepository.GetAsync(command.TodoListId) is var todoList && todoList == null)
            return new GenericCommandResult(false, "Lista inválida", null);

        await _todoListRepository.DeleteAsync(todoList);

        return new GenericCommandResult(true, "Lista deletada", null);
    }
}