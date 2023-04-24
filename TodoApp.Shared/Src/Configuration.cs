namespace TodoApp.Shared.Src;

public class Configuration
{
    public static string ApiKey = "";
    public static string JwtKey = "";
    public static string SqliteDbName = "todoapp.db";
    public static ConnectionStringsConfiguration ConnectionStrings = new();

    public class ConnectionStringsConfiguration
    {
        public string DefaultConnection { get; set; } = "";
    }
}