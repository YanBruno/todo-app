using Dapper;
using System.Data;
using System.Text;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Repositories;
using TodoApp.Infra.Src.DataContext;
using TodoApp.Infra.Src.QueryResults;
using TodoApp.Infra.Src.SqliteUtils;

namespace TodoApp.Infra.Src.Repositories;

public class TodoListRepository : BaseRepository, ITodoListRepository
{
    public TodoListRepository(TodoAppContext context) : base(context)
    {
    }

    public async Task<bool> CheckExists(Guid todoListId)
    {
        var result = await Context
            .Connection
            .QueryFirstOrDefaultAsync<int>(
                "select count(1) from tb_todo_list where TodoListId = @todoListId"
                , new { todoListId }
                , commandType: CommandType.Text
            );

        if (result > 0)
        {
            return true;
        }
        else { return false; }

    }

    public async Task<bool> CreateAsync(TodoList todoList)
    {
        try
        {
            await Context
            .Connection
            .ExecuteAsync(Scripts.InserTodoList, new
            {
                todoListId = todoList.Id.ToString(),
                createdAt = todoList.CreatedAt,
                title = todoList.Title.Value,
                description = todoList.Description.Value,
                userId = todoList.GetUser().Id.ToString()
            }
            , commandType: CommandType.Text
        );

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public async Task<bool> DeleteAsync(TodoList todoList)
    {
        var script = new StringBuilder();
        script.AppendLine("delete from tb_todo_item where TodoListId = @todoListId;");
        script.AppendLine("delete from tb_todo_list where TodoListId = @todoListId;");

        try
        {
            await Context
            .Connection
            .ExecuteAsync(
                script.ToString()
                , new
                {
                    todoListId = todoList.Id.ToString()
                }
                , commandType: CommandType.Text
        );

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public async Task<TodoList?> GetAsync(Guid todoListId)
    {
        var script = new StringBuilder();

        script.AppendLine("select t2.* from tb_todo_list t1 inner join tb_user t2 on t1.userid = t2.userid where t1.TodoListId = @todoListId;");
        script.AppendLine("select t1.* from tb_todo_list t1 where t1.TodoListId = @todoListId;");
        script.AppendLine("select * from tb_todo_item where TodoListId = @TodoListId");

        var multi = await Context
            .Connection
            .QueryMultipleAsync(
                script.ToString()
                , new { todoListId = todoListId.ToString() }
                , commandType: CommandType.Text
            );

        var userResult = multi.ReadSingleOrDefault<UserQueryResult>();
        var todoListResult = multi.ReadSingleOrDefault<TodoListQueryResult>();
        var todoItemsResult = multi.Read<TodoItemQueryResult>();

        if (todoListResult == null)
            return null;

        var todoList = todoListResult.Build(userResult.Build());

        foreach (var todoItemResult in todoItemsResult)
        {
            todoList.AddTodoItem(
                todoItemResult.Build(todoList)    
            );
        }

        return todoList;
    }

    public async Task<IEnumerable<TodoList>> GetByUserIdAsync(Guid userId)
    {
        var script = new StringBuilder();

        script.AppendLine("select * from tb_user where UserId = @userId;");
        script.AppendLine("select * from tb_todo_list where userId = @userId;");

        var todoLists = new List<TodoList>();

        var multi = await Context
            .Connection
            .QueryMultipleAsync(
                script.ToString()
                , new { userId = userId.ToString() }
                , commandType: CommandType.Text
            );

        var userResult = multi.ReadSingleOrDefault<UserQueryResult>();
        var todoListsResult = multi.Read<TodoListQueryResult>();

        if (userResult != null)
            foreach (var todoListResult in todoListsResult)
                todoLists.Add(todoListResult.Build(userResult.Build()));

        return todoLists;
    }

    public Task<bool> UpdateAsync(TodoList todoList)
    {
        throw new NotImplementedException();
    }
}