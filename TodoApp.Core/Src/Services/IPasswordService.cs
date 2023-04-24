namespace TodoApp.Core.Src.Services;

public interface IPasswordService
{
    public string GenerateHash(string password);
    public bool CheckPassword(string hash, string password);
}
