namespace TodoApp.Core.Src.Commands.TodoItemCommands;

public class CreateTodoItemCommand : Command
{
    public CreateTodoItemCommand(Guid todoListId, string title, string description)
    {
        TodoListId = todoListId;
        Title = title;
        Description = description;
    }

    public Guid TodoListId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public override void Validate()
    {
        //Todo criar validacao
    }
}
