using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Entities;

namespace TodoApp.Core.Src.Entities;

public class TodoList : Entity
{
    private readonly IList<TodoItem> _todos = new List<TodoItem>();
    private readonly User _user;

    public TodoList(Title title, Description description, User user) => (Title, Description, _user) = (title, description, user);

    public Title Title { get; private set; }
    public Description Description { get; private set; }

    public IReadOnlyCollection<TodoItem> Todos => _todos.ToArray();

    public void AddTodoItem(TodoItem todoItem) => _todos.Add(todoItem);

    public void RemoveTodoItem(TodoItem todoItem) => _todos.Remove(todoItem);

    public void CleanTodos() => _todos.Clear();

    public User GetUser() => _user;
}