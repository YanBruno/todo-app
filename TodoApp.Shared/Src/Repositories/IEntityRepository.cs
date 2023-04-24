using TodoApp.Shared.Src.Entities;

namespace TodoApp.Shared.Src.Repositories
{
    public interface IEntityRepository<T> where T : Entity
    {
        public Task<bool> CreateAsync(T entity);
        public Task<bool> DeleteAsync(T entity);
        public Task<bool> UpdateAsync(T entity);

        public Task<T?> GetAsync(Guid entityId);
        public Task<bool> CheckExists(Guid entityId);
    }
}