namespace TodoApp.Core.Src.Commands.TodoListCommands;

public class CreateTodoListCommand : Command
{
    public CreateTodoListCommand(Guid userId, string title, string description)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }

    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public override void Validate()
    {
        //todo criar validacao
    }
}
