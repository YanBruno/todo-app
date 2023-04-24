using Flunt.Notifications;
using TodoApp.Shared.Src.Commands;

namespace TodoApp.Core.Src.Commands
{
    public abstract class Command : Notifiable<Notification>, ICommand
    {
        public abstract void Validate();
    }
}
