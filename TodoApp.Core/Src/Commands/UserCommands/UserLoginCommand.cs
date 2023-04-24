namespace TodoApp.Core.Src.Commands.UserCommands;

public class UserLoginCommand : Command
{
    public UserLoginCommand(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }

    public string EmailAddress { get; set; }
    public string Password { get; set; }


    public override void Validate()
    {
        //todo criar validacao
    }
}
