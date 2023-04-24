using TodoApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureContext();
builder.ConfigureRepositories();
builder.ConfigureHandlers();
builder.ConfigureServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.LoadConfiguration();
app.LoadSqlite();

// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("ShowSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();