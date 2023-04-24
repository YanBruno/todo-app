using TodoApp.Core.Src.Entities;
using TodoApp.Shared.Src.Repositories;

namespace TodoApp.Core.Src.Repositories;

public interface ITodoListRepository : IEntityRepository<TodoList>
{
    public Task<IEnumerable<TodoList>> GetByUserIdAsync(Guid userId);
}