namespace TodoApp.Core.Src.Commands.TodoItemCommands
{
    public class UpdateTodoItemCommand : Command
    {
        public UpdateTodoItemCommand(Guid todoItemId, string newTitle, string newDescription)
        {
            TodoItemId = todoItemId;
            NewTitle = newTitle;
            NewDescription = newDescription;
        }

        public Guid TodoItemId { get; set; }
        public string NewTitle { get; set; }
        public string NewDescription { get; set; }

        public override void Validate()
        {
            //todo criar validacao
        }
    }
}
