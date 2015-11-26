using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;

namespace WindowsTime.UnitTest.DAO.UsuarioRepositoryTestes
{
    [TestClass]
    public class Ao_obter_usuario_existente : AbstractDaoTesteAutoAct
    {
        public override void Arrange()
        {
        }

        public override void Act()
        {
        }

        [TestMethod]
        public void Deve_obter_com_sucesso()
        {
            var usuario = new UsuarioRepository().ObterUsuarioPorEmail("thiagomenezes2k7@gmail.com");

            Assert.IsNotNull(usuario);
            Assert.AreEqual(1, usuario.Id);
            Assert.AreEqual("thiagomenezes2k7@gmail.com", usuario.Email);
            Assert.AreEqual("TMenezes", usuario.Nome);
        }
    }
}