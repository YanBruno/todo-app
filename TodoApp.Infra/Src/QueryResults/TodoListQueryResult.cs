using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Utils;
using TodoApp.Core.Src.ValueObjects;

namespace TodoApp.Infra.Src.QueryResults;

public class TodoListQueryResult
{
    public TodoListQueryResult(string todoListId, DateTime createdAt, string title, string description, string userId)
    {
        TodoListId = todoListId;
        CreatedAt = createdAt;
        Title = title;
        Description= description;
        UserId = userId;
    }

    public string TodoListId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }

    public TodoList Build(User user)
    {
        var todoList = new TodoList(
            new Title(Title)
            , new Description(Description)
            , user
        );

        Reflections.SetEntityProps(todoList, Guid.Parse(TodoListId), CreatedAt);
        return todoList;
    }
}
