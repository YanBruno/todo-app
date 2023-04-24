using Flunt.Notifications;
using TodoApp.Core.Src.Commands.TodoItemCommands;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Repositories;
using TodoApp.Core.Src.Utils;
using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Commands;
using TodoApp.Shared.Src.Handlers;

namespace TodoApp.Core.Src.Handlers;

public class TodoItemHandler
    : Notifiable<Notification>
    , IHandler<CreateTodoItemCommand>
    , IHandler<DeleteTodoItemCommand>
    , IHandler<ChangeDoneItemCommand>
    , IHandler<UpdateTodoItemCommand>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoItemHandler(ITodoListRepository todoListRepository, ITodoItemRepository todoItemRepository)
    {
        _todoListRepository = todoListRepository;
        _todoItemRepository = todoItemRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreateTodoItemCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command)) return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _todoListRepository.GetAsync(command.TodoListId) is var todoList && todoList == null)
            return new GenericCommandResult(false, "Lista inválida", null);

        var title = new Title(command.Title);
        var description = new Description(command.Description);

        var todoItem = new TodoItem(title, description, todoList);

        todoList.AddTodoItem(todoItem);

        AddNotifications(title, description, todoItem, todoList);

        if (!IsValid)
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        await _todoItemRepository.CreateAsync(todoItem);

        return new GenericCommandResult(true, "Lista criada com sucesso", new { todoItem });
    }

    public async Task<ICommandResult> HandleAsync(DeleteTodoItemCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command))
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _todoItemRepository.GetAsync(command.TodoItemId) is var todoItem && todoItem == null)
            return new GenericCommandResult(false, "Todo inváido", null);

        await _todoItemRepository.DeleteAsync(todoItem);

        return new GenericCommandResult(true, "Todo removido", null);
    }

    public async Task<ICommandResult> HandleAsync(ChangeDoneItemCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command))
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _todoItemRepository.GetAsync(command.TodoItemId) is var todoItem && todoItem == null)
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        todoItem.MarkDone();
        await _todoItemRepository.UpdateDoneAsync(todoItem);

        return new GenericCommandResult(true, "Todo atualziado", todoItem);
    }

    public async Task<ICommandResult> HandleAsync(UpdateTodoItemCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command))
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _todoItemRepository.GetAsync(command.TodoItemId) is var todoItem && todoItem == null)
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);


        var title = new Title(command.NewTitle);
        var description = new Description(command.NewDescription);

        todoItem.UpdateTitle(title);
        todoItem.UpdateDescription(description);

        await _todoItemRepository.UpdateAsync(todoItem);

        return new GenericCommandResult(true, "Todo atualziado", todoItem);
    }
}