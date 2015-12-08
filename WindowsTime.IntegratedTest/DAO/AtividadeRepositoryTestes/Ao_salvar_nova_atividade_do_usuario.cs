using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.Core.Dominio;
using WindowsTime.DAO;
using WindowsTime.UnitTest;

namespace WindowsTime.IntegratedTest.DAO.AtividadeRepositoryTestes
{
    [TestClass]
    public class Ao_salvar_nova_atividade_do_usuario : AbstractDaoTesteAutoAct
    {
        public override void Arrange()
        {
        }

        public override void Act()
        {
        }

        [TestMethod]
        public void Deve_salvar_programa_com_sucesso()
        {
            var usuario = new Usuario() { Id = 1 };
            var programa = new ProgramaRepository().GetById(1);

            var atividade = StubHelper.NovoAtividadeDoUsuario(usuario, programa, qtdeJanelas: 2);
            var atividadeRepository = new AtividadeDoUsuarioRepository();

            atividadeRepository.SaveAndPersist(atividade);
        }
    }
}