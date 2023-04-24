using Dapper;
using System.Data;
using System.Text;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Repositories;
using TodoApp.Infra.Src.DataContext;
using TodoApp.Infra.Src.QueryResults;

namespace TodoApp.Infra.Src.Repositories;

public class TodoItemRepository : BaseRepository, ITodoItemRepository
{
    public TodoItemRepository(TodoAppContext context) : base(context)
    {
    }

    public async Task<bool> CheckExists(Guid entityId)
    {
        var result = await Context
            .Connection
            .QueryFirstOrDefaultAsync<int>(
                "select count(1) from tb_todo_item where TodoItemId = @entityId"
                , new { entityId }
                , commandType: CommandType.Text
            );

        if (result > 0)
        {
            return true;
        }
        else { return false; }

    }

    public async Task<bool> CreateAsync(TodoItem entity)
    {
        try
        {
            await Context
                .Connection
                .ExecuteAsync(
                    SqliteUtils.Scripts.InserTodoItem
                    , new
                    {
                        TodoItemId = entity.Id.ToString(),
                        createdAt = entity.CreatedAt,
                        title = entity.Title.Value,
                        description = entity.Description.Value,
                        done = entity.Done,
                        TodoListId = entity.GetTodoList().Id.ToString()
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

    public async Task<bool> DeleteAsync(TodoItem entity)
    {
        try
        {
            await Context
                .Connection
                .ExecuteAsync(
                    "delete from tb_todo_item where TodoItemId = @todoItemId"
                    , new { todoItemId = entity.Id.ToString() }
                    , commandType: CommandType.Text
                );

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<TodoItem?> GetAsync(Guid entityId)
    {
        var script = new StringBuilder();

        script.AppendLine("select t3.* from tb_todo_item t1 inner join tb_todo_list t2 ON t1.TodoListId = t2.TodoListId inner join tb_user t3 on t2.UserId = t3.UserId where t1.TodoItemId = @todoItemId;");
        script.AppendLine("select t2.* from tb_todo_item t1 inner join tb_todo_list t2 ON t1.TodoListId = t2.TodoListId where t1.TodoItemId = @todoItemId;");
        script.AppendLine("select t1.* from tb_todo_item t1 where t1.TodoItemId = @todoItemId;");

        var multi = await Context
            .Connection
            .QueryMultipleAsync(
                script.ToString()
                , new { todoItemId = entityId.ToString() }
                , commandType: CommandType.Text
            );

        var userResult = multi.ReadSingleOrDefault<UserQueryResult>();
        var todoListResult = multi.ReadSingleOrDefault<TodoListQueryResult>();
        var todoItemResult = multi.ReadSingleOrDefault<TodoItemQueryResult>();

        if (todoItemResult == null)
            return null;

        var todoItem = todoItemResult.Build(todoListResult.Build(userResult.Build()));

        return todoItem;
    }

    public async Task<bool> UpdateAsync(TodoItem entity)
    {
        try
        {
            await Context
                .Connection
                .ExecuteAsync(
                    "update tb_todo_item set Title = @Title, Description = @Description where TodoItemId = @todoItemId"
                    , new { Title = entity.Title.Value, Description = entity.Description.Value, todoItemId = entity.Id.ToString() }
                    , commandType: CommandType.Text
                );

            return true;
        }
        catch (Exception)
        {

            return false;
        }

    }

    public async Task<bool> UpdateDoneAsync(TodoItem todoItem)
    {
        try
        {
            await Context
                .Connection
                .ExecuteAsync(
                    "update tb_todo_item set Done = @Done where TodoItemId = @todoItemId"
                    , new { todoItem.Done, todoItemId = todoItem.Id.ToString() }
                    , commandType: CommandType.Text
                );

            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }
}
