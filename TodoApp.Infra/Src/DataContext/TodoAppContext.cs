using System.Data.SQLite;
using TodoApp.Shared.Src;

namespace TodoApp.Infra.Src.DataContext;

public class TodoAppContext : IDisposable
{
    public TodoAppContext() => Connection = new SQLiteConnection(Configuration.ConnectionStrings.DefaultConnection);

    public SQLiteConnection Connection { get; private set; }

    public void Dispose()
    {
        if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
    }
}