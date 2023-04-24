using TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;
using TodoApp.Shared.Src.ValueObjects;

namespace TodoApp.Core.Src.ValueObjects;

public class Description : ValueObject
{
    public Description(string value)
    {

        Value = value;
        AddNotifications(new CreateDescriptionContract(this));
    }

    public string Value { get; private set; }

    public static explicit operator string(Description description) => description.Value;

    public override string ToString() => Value;
}