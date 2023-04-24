using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Src.Commands.TodoListCommands;
using TodoApp.Core.Src.Handlers;
using TodoApp.Core.Src.Repositories;

namespace TodoApp.Api.Controllers;

[Route("api")]
[ApiController]
public class TodoListController : ControllerBase
{
    private readonly TodoListHandler _handler;
    private readonly ITodoListRepository _repository;

    public TodoListController(TodoListHandler handler, ITodoListRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [HttpGet("v1/[controller]/{todoListId:Guid}")]
    public async Task<IActionResult> Get(Guid todoListId) => Ok(await _repository.GetAsync(todoListId));

    [HttpGet("v1/[controller]/all/{userId:Guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId) => Ok(await _repository.GetByUserIdAsync(userId));

    [HttpPost("v1/[controller]")]
    public async Task<IActionResult> Create([FromBody] CreateTodoListCommand command) => Ok(await _handler.HandleAsync(command));

    [HttpDelete("v1/[controller]")]
    public async Task<IActionResult> Delete([FromBody] DeleteTodoListCommand command) => Ok(await _handler.HandleAsync(command));
}