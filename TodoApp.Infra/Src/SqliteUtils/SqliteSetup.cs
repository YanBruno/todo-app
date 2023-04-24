using Dapper;
using System.Data;
using TodoApp.Infra.Src.DataContext;
using TodoApp.Shared.Src;

namespace TodoApp.Infra.Src.SqliteUtils;

public class SqliteSetup : IBootstrapSetup
{
    public void Apply()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var fullPath = Path.Combine(currentDir, Configuration.SqliteDbName);

        if (File.Exists(fullPath)) File.Delete(fullPath);
        using var context = new TodoAppContext();

        context.Connection.AddTypeMapping("bit", DbType.Boolean, true);
        context.Connection.AddTypeMapping("uniqueidentifier", DbType.String, true);

        context.Connection.Execute(Scripts.CreateTables, commandType: CommandType.Text);
    }
}
