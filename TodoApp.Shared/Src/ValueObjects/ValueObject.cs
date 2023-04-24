using Flunt.Notifications;

namespace TodoApp.Shared.Src.ValueObjects;

public abstract class ValueObject : Notifiable<Notification>
{
    public abstract override string ToString();
}