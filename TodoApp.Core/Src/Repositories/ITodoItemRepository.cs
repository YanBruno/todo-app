using TodoApp.Core.Src.Entities;
using TodoApp.Shared.Src.Repositories;

namespace TodoApp.Core.Src.Repositories;

public interface ITodoItemRepository : IEntityRepository<TodoItem>
{
    public Task<bool> UpdateDoneAsync(TodoItem todoItem);
}
