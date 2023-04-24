using TodoApp.Core.Src.Extensions;
using TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;
using TodoApp.Shared.Src.ValueObjects;

namespace TodoApp.Core.Src.ValueObjects;

public class Title : ValueObject
{
    public Title(string value)
    {
        Value = value.FirstLetterUpper();

        AddNotifications(new CreateTitleContract(this));
    }
 
    public string Value { get; private set; }

    public static explicit operator string(Title title) => title.Value;

    public override string ToString() => Value;
}
