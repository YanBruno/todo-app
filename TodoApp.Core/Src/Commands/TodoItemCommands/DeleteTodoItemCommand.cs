namespace TodoApp.Core.Src.Commands.TodoItemCommands;

public class DeleteTodoItemCommand : Command
{
    public DeleteTodoItemCommand(Guid todoItemId) => TodoItemId = todoItemId;

    public Guid TodoItemId { get; set; }

    public override void Validate()
    {
        //todo criar validacao
    }
}
