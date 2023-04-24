namespace TodoApp.Core.Src.Extensions;

public static class AppExtensions
{
    public static string FirstLetterUpper(this string text) => string.Concat(text[0].ToString().ToUpper(), text[1..].ToLower());
}
