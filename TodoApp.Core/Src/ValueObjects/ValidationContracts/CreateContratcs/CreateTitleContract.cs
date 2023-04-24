using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;

public class CreateTitleContract : Contract<Notification>
{
    public CreateTitleContract(Title title)
    {
        Requires()
            .IsGreaterOrEqualsThan(title.Value, 3, "Title.Value");
    }
}
