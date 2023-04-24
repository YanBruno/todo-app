using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Entities;

namespace TodoApp.Core.Src.Entities;

public class User : Entity
{
    private readonly IList<TodoList> _lists = new List<TodoList>();
    private readonly Password _password;

    public User(Name name, Email email, Password password)
    {
        (Name, Email, _password) = (name, email, password);
        Active = true;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public bool Active { get; private set; }

    public IReadOnlyCollection<TodoList> Lists => _lists.ToArray();

    public void AddTodoList(TodoList todoList) => _lists.Add(todoList);
    public void RemoveList(TodoList todoList) => _lists.Remove(todoList);

    public void DeactiveAccount() => Active = false;

    public Password GetPassword() => _password;
}