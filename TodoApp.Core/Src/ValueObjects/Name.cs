using TodoApp.Core.Src.Extensions;
using TodoApp.Core.Src.ValueObjects.ValidationContracts.CreateContratcs;
using TodoApp.Shared.Src.ValueObjects;

namespace TodoApp.Core.Src.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName) { 
        (FirstName, LastName) = (firstName.FirstLetterUpper(), lastName.FirstLetterUpper());

        AddNotifications(new CreateNameContract(this));
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public static implicit operator string(Name name) => $"{name.FirstName} {name.LastName}";
    public override string ToString() => $"{FirstName} {LastName}";
}