namespace TodoApp.Core.Src.Commands.UserCommands
{
    public class CreateUserCommand : Command
    {
        public CreateUserCommand(string firstName, string lastName, string emailAddress, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
            //Todo realizar validação

        }
    }
}
