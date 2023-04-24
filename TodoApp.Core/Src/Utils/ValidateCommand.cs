using TodoApp.Core.Src.Commands;

namespace TodoApp.Core.Src.Utils
{
    public class ValidateCommand
    {
        public static bool IsCommandValid(Command command)
        {
            command.Validate(); return command.IsValid;
        }
    }
}
