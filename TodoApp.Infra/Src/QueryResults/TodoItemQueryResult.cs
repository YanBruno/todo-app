using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Utils;
using TodoApp.Core.Src.ValueObjects;

namespace TodoApp.Infra.Src.QueryResults;

public class TodoItemQueryResult
{
    public TodoItemQueryResult(string todoItemId, DateTime createdAt, string title, string description, bool done, string todoListId)
    {
        TodoItemId = todoItemId;
        CreatedAt = createdAt;
        Title = title;
        Description = description;
        Done = done;
        TodoListId = todoListId;
    }

    public string TodoItemId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
    public string TodoListId { get; set; }

    public TodoItem Build(TodoList todoList)
    {
        var todo = new  TodoItem(
            new Title(Title)
            , new Description(Description)
            , todoList
        );

        Reflections.SetEntityProps(todo, Guid.Parse(TodoItemId), CreatedAt);
        Reflections.SetTodoItemProps(todo, Done);
        return todo;
    }
}
