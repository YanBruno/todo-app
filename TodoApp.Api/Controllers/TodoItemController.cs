using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Src.Commands.TodoItemCommands;
using TodoApp.Core.Src.Handlers;
using TodoApp.Core.Src.Repositories;

namespace TodoApp.Api.Controllers;

[Route("api")]
[ApiController]
public class TodoItemController : ControllerBase
{
    private readonly TodoItemHandler _handler;
    private readonly ITodoItemRepository _repository;

    public TodoItemController(TodoItemHandler handler, ITodoItemRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [HttpGet("v1/[controller]/{todoItemId:Guid}")]
    public async Task<IActionResult> Get(Guid todoItemId) => Ok(await _repository.GetAsync(todoItemId));

    [HttpPost("v1/[controller]")]
    public async Task<IActionResult> Create([FromBody] CreateTodoItemCommand command) => Ok(await _handler.HandleAsync(command));

    [HttpPut("v1/[controller]")]
    public async Task<IActionResult> Update([FromBody] UpdateTodoItemCommand command) => Ok(await _handler.HandleAsync(command));

    [HttpPut("v1/[controller]/done")]
    public async Task<IActionResult> MarkDone([FromBody] ChangeDoneItemCommand command) => Ok(await _handler.HandleAsync(command));

    [HttpDelete("v1/[controller]")]
    public async Task<IActionResult> Delete([FromBody] DeleteTodoItemCommand command) => Ok(await _handler.HandleAsync(command));
}
