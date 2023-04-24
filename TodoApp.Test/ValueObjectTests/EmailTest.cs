using TodoApp.Core.Src.ValueObjects;

namespace TodoApp.Test.ValueObjectTests;

[TestClass]
[TestCategory("ValueObject")]
public class EmailTest
{
    [TestMethod]
    public void Deve_retornar_0_notifications_dado_email_valido() {
        Assert.AreEqual(0, new Email("yanbrunosilvasantos@gmail.com").Notifications.Count);
    }

    [TestMethod]
    public void Deve_retornar_1_notifications_dado_email_invalido()
    {
        Assert.AreEqual(1, new Email("yanbrunosilvasantos@gmail").Notifications.Count);
    }
}
