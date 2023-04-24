using SecureIdentity.Password;
using TodoApp.Core.Src.Services;

namespace TodoApp.Infra.Src.Services;

public class PasswordService : IPasswordService
{
    public bool CheckPassword(string hash, string password) => PasswordHasher.Verify(hash, password);

    public string GenerateHash(string password) => PasswordHasher.Hash(password);
}
