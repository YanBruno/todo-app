namespace TodoApp.Core.Src.Commands.TodoItemCommands
{
    public class ChangeDoneItemCommand : Command
    {
        public ChangeDoneItemCommand(Guid todoItemId)
        {
            TodoItemId = todoItemId;
        }

        public Guid TodoItemId { get; set; }

        public override void Validate()
        {
            //todo criar validacao
        }
    }
}
