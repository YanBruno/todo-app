using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoApp.Core.Src.Handlers;
using TodoApp.Core.Src.Repositories;
using TodoApp.Core.Src.Services;
using TodoApp.Infra.Src.DataContext;
using TodoApp.Infra.Src.Repositories;
using TodoApp.Infra.Src.Services;
using TodoApp.Infra.Src.SqliteUtils;
using TodoApp.Shared.Src;

namespace TodoApp.Api.Extensions;

public static class AppExtensions
{
    public static void ConfigureHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<UserHandler>();
        builder.Services.AddTransient<TodoListHandler>();
        builder.Services.AddTransient<TodoItemHandler>();
    }

    public static void ConfigureContext(this WebApplicationBuilder builder) => builder.Services.AddScoped<TodoAppContext>();

    public static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IBootstrapSetup, SqliteSetup>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ITodoListRepository, TodoListRepository>();
        builder.Services.AddTransient<ITodoItemRepository, TodoItemRepository>();
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IPasswordService, PasswordService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
    }

    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    public static void LoadSqlite(this WebApplication app) => app.Services.GetService<IBootstrapSetup>()!.Apply();

    public static void LoadConfiguration(this WebApplication app)
    {
        Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");

        var connectionStrings = new Configuration.ConnectionStringsConfiguration();
        app.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
        Configuration.ConnectionStrings = connectionStrings;
    }
}