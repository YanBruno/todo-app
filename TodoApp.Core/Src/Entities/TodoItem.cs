using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Entities;

namespace TodoApp.Core.Src.Entities;

public class TodoItem : Entity
{
    private readonly TodoList _todoList;

    public TodoItem(Title title, Description description, TodoList todoList)
    {
        (Title, Description, _todoList) = (title, description, todoList);
        Done = false;
    }

    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public bool Done { get; private set; }

    public void UpdateTitle(Title title) => Title = title;
    public void UpdateDescription(Description description) => Description = description;

    public void MarkDone() => Done = !Done;
    public TodoList GetTodoList() => _todoList;
}