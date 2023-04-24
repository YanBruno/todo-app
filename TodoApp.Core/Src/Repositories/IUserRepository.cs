using TodoApp.Core.Src.Entities;
using TodoApp.Shared.Src.Repositories;

namespace TodoApp.Core.Src.Repositories;

public interface IUserRepository : IEntityRepository<User>
{
    public Task<User?> GetAsync(string emailAddress);
    public Task<bool> CheckEmail(string emailAddress);
}
