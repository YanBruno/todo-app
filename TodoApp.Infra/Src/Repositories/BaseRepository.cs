using TodoApp.Infra.Src.DataContext;

namespace TodoApp.Infra.Src.Repositories;

public abstract class BaseRepository
{
    protected BaseRepository(TodoAppContext context) => Context = context;
    public TodoAppContext Context { get; private set; }
}
