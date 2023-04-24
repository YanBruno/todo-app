namespace TodoApp.Core.Src.Commands.UserCommands;

public class DeleteUserCommand : Command
{
    public DeleteUserCommand(Guid userId, string password)
    {
        UserId = userId;
        Password = password;
    }

    public Guid UserId { get; set; }
    public string Password { get; set; }

    public override void Validate()
    {
        //Todo
    }
}
