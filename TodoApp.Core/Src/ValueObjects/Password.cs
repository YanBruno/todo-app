using TodoApp.Shared.Src.ValueObjects;

namespace TodoApp.Core.Src.ValueObjects;

public class Password : ValueObject
{
    public Password(string hash) => Hash = hash;
    
    public string Hash { get; private set; }

    public static implicit operator string(Password pass) => pass.Hash;

    public override string ToString() => Hash;

    public void HideHash() => Hash = "******";
}