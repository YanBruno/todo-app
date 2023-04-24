using TodoApp.Core.Src.ValueObjects;

namespace TodoApp.Test.ValueObjectTests;

[TestClass]
[TestCategory("ValueObject")]
public class TitleTest
{
    [TestMethod]
    public void Deve_retornar_zero_notificacoes_dado_titutlo_valido() { 
        Assert.AreEqual(0, new Title("Tarefa 01").Notifications.Count);
    }

    [TestMethod]
    public void Deve_retornar_uma_notificacoes_dado_titutlo_invalido()
    {
        Assert.AreEqual(1, new Title("Ta").Notifications.Count);
    }
}
