using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;
using WindowsTime.UnitTest;

namespace WindowsTime.IntegratedTest.DAO.UsuarioRepositoryTestes
{
    [TestClass]
    public class Ao_salvar_novo_usuario
    {
        [TestMethod]
        public void Deve_salvar_usuario_com_sucesso()
        {
            var usuario = StubHelper.NovoUsuario();
            var usuarioRepository = new UsuarioRepository();

            usuarioRepository.Save(usuario);
        }
    }
}
