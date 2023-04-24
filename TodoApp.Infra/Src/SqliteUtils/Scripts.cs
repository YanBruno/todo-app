namespace TodoApp.Infra.Src.SqliteUtils;

public class Scripts
{
    public readonly static string CreateTableUser = @"
CREATE TABLE tb_user (
    UserId TEXT PRIMARY KEY,
    CreatedAt DATE,
    FirstName TEXT,
    LastName TEXT,
    Email TEXT,
    PasswordHash TEXT,
    Active bit
);
";

    public readonly static string CreateTableTodoList = @"
CREATE TABLE tb_todo_list (
    TodoListId TEXT PRIMARY KEY,
    CreatedAt DATE,
    title TEXT,
    description TEXT,
    userId TEXT REFERENCES tb_user(UserId)
);
";

    public readonly static string CreatetableTodoItem = @"
CREATE TABLE tb_todo_item (
    TodoItemId TEXT PRIMARY KEY,
    CreatedAt DATE,
    Title TEXT,
    Description TEXT,
    Done bit,
    TodoListId TEXT REFERENCES tb_todo_list(TodoListId)
);
";

    public readonly static string InsertUser = @"
INSERT INTO tb_user VALUES (@UserId, @createdAt, @firstName, @lastName, @email, @hash, @active);

";

    public readonly static string InserTodoList = @"
INSERT INTO tb_todo_list VALUES (@todoListId, @createdAt, @title, @description, @userId );

";

    public readonly static string InserTodoItem = @"
INSERT INTO tb_todo_item VALUES (@TodoItemId, @createdAt, @title, @description, @done, @TodoListId );

";
    public readonly static string CreateTables = $"{CreateTableUser} {CreateTableTodoList} {CreatetableTodoItem}";
}
