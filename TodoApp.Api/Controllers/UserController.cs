using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Src.Commands.TodoItemCommands;
using TodoApp.Core.Src.Commands.UserCommands;
using TodoApp.Core.Src.Handlers;
using TodoApp.Shared.Src.Commands;

namespace TodoApp.Api.Controllers;

[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserHandler _handler;

    public UserController(UserHandler handler) => _handler = handler;

    [HttpPost("v1/[controller]")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command) => Ok(await _handler.HandleAsync(command));

    [HttpPost("v1/login")]
    public Task<ICommandResult> Login([FromBody] UserLoginCommand command) => _handler.HandleAsync(command);
}
