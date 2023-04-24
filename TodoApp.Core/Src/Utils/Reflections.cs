using System;
using System.Reflection;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Entities;

namespace TodoApp.Core.Src.Utils;

public class Reflections
{
    public static void SetEntityProps(Entity entity, Guid id, DateTime createdAt)
    {
        var baseType = entity
            .GetType()
            .BaseType!;

        var idProp = baseType.GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;
        var createdAtProp = baseType.GetProperty("CreatedAt", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;

        idProp.SetValue(entity, id);
        createdAtProp.SetValue(entity, createdAt);
    }

    public static void SetUserProps(User user, bool active)
    {
        var activeProp = user.GetType().GetProperty("Active", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;
        activeProp.SetValue(user, active);
    }

    public static void SetTodoItemProps(TodoItem todoItem, bool done)
    {
        var prop = todoItem.GetType().GetProperty("Done", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;
        prop.SetValue(todoItem, done);
    }

    public static void SetEmailProps(Email email, bool verified, string verificationCode, DateTime verificationCodeExpireDate)
    {
        var verifiedProp = email.GetType().GetProperty("Verified", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;
        var verificationCodeProp = email.GetType().GetProperty("VerificationCode", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;
        var verificationCodeExpireDateProp = email.GetType().GetProperty("VerificationCodeExpireDate", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;

        verifiedProp.SetValue(email, verified);
        verificationCodeProp.SetValue(email, verificationCode);
        verificationCodeExpireDateProp.SetValue(email, verificationCodeExpireDate);
    }
}
