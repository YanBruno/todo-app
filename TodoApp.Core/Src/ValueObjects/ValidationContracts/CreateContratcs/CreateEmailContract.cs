using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;

public class CreateEmailContract : Contract<Notification>
{
    public CreateEmailContract(Email item)
    {
        Requires()
            .IsEmail(item.Address, "Email.Address");
    }
}
