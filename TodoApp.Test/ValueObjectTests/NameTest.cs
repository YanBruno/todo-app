using TodoApp.Core.Src.ValueObjects;

namespace TodoApp.Test.ValueObjectTests;

[TestClass]
[TestCategory("ValueObject")]
public class NameTest
{
    [TestMethod]
    public void Deve_retornar_nome_valido_dado_nenhuma_notification() => Assert.IsTrue(new Name("Yan", "Santos").IsValid);

    [TestMethod]
    public void Deve_retornar_uma_notificacao_dado_first_name_invalidos() => Assert.AreEqual(1, new Name("Ya", "Santos").Notifications.Count);

    [TestMethod]
    public void Deve_retornar_duas_notificacoes_dado_first_e_last_name_invalidos() => Assert.AreEqual(2, new Name("Ya", "Ss").Notifications.Count);
}
