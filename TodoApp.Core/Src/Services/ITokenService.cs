using TodoApp.Core.Src.Entities;

namespace TodoApp.Core.Src.Services;

public interface ITokenService
{
    public string GenerateToken(User user);
}
