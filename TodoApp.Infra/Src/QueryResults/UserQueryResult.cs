using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.ValueObjects;

namespace TodoApp.Infra.Src.QueryResults;

public class UserQueryResult
{
    public UserQueryResult(string userId, DateTime createdAt, string firstName, string lastName, string email, string passwordHash, bool active)
    {
        UserId = userId;
        CreatedAt = createdAt;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Active = active;
    }

    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool Active { get; set; }

    public User Build()
    {
        var user = new User(
            new Name(FirstName, LastName)
            , new Email(Email)
            , new Password(PasswordHash)
        );

        Core.Src.Utils.Reflections.SetEntityProps(user, Guid.Parse(UserId), CreatedAt);
        Core.Src.Utils.Reflections.SetUserProps(user, Active);

        //Todo
        //Core.Src.Utils.Reflections.SetEmailProps(user, Verifi);

        return user;
    }
}
