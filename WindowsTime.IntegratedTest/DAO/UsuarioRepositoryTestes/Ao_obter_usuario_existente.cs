using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;

namespace WindowsTime.IntegratedTest.DAO.UsuarioRepositoryTestes
{
    [TestClass]
    public class Ao_obter_usuario_existente
    {
        [TestMethod]
        public void Deve_obter_com_sucesso()
        {
            var usuario = new UsuarioRepository().ObterUsuarioPorEmail("thiagomenezes2k7@gmail.com");

            Assert.IsNotNull(usuario);
            Assert.AreEqual<int>(1, usuario.Id);
            Assert.AreEqual<string>("thiagomenezes2k7@gmail.com", usuario.Email);
            Assert.AreEqual<string>("TMenezes", usuario.Nome);
        }
    }
}