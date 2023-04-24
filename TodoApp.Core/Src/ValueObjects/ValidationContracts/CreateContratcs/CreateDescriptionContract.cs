using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;

public class CreateDescriptionContract : Contract<Notification>
{
    public CreateDescriptionContract(Description item)
    {
        Requires()
            .IsGreaterOrEqualsThan(item.Value.Length, 8, "Description.Value");
    }
}
