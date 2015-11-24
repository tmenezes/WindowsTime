using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;

namespace WindowsTime.UnitTest.DAO.UsuarioRepositoryTestes
{
    [TestClass]
    public class Ao_salvar_novo_usuario : AbstractDaoTesteAutoAct
    {
        public override void Arrange()
        {
        }

        public override void Act()
        {
        }

        [TestMethod]
        public void Deve_salvar_usuario_com_sucesso()
        {
            var usuario = StubHelper.NovoUsuario();
            var usuarioRepository = new UsuarioRepository();

            usuarioRepository.Save(usuario);
        }
    }
}
