using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;

public class CreateNameContract : Contract<Notification>
{
    public CreateNameContract(Name name)
    {
        Requires()
            .IsGreaterOrEqualsThan(name.FirstName, 3, "Name.FirstName")
            .IsGreaterOrEqualsThan(name.LastName, 3, "Name.LastName");
    }
}
