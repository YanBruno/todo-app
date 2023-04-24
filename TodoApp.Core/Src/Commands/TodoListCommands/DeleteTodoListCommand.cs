namespace TodoApp.Core.Src.Commands.TodoListCommands
{
    public class DeleteTodoListCommand : Command
    {
        public DeleteTodoListCommand(Guid todoListId, Guid userId)
        {
            TodoListId = todoListId;
            UserId = userId;
        }

        public Guid TodoListId { get; set; }
        public Guid UserId { get; set; }

        public override void Validate()
        {
            //todo criar validacao
        }
    }
}
