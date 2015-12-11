using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using WindowsTime.Core.Dominio;
using WindowsTime.DAO;

namespace WindowsTime.IntegratedTest.DAO.AtividadeRepositoryTestes
{
    [TestClass]
    public class Ao_obter_atividade_do_dia
    {
        [TestMethod]
        public void Deve_salvar_programa_com_sucesso()
        {
            var usuario = new Usuario() { Id = 1 };
            var atividadeRepository = new AtividadeDoUsuarioRepository();

            var atividade = atividadeRepository.ObterAtividadeDoUsuarioDoDia(usuario);

            Assert.IsNotNull(atividade);
            Assert.IsNotNull(atividade.Usuario);

            Assert.IsFalse(NHibernateUtil.IsInitialized(atividade.Janelas));
            Assert.IsNotNull(atividade.Janelas);
        }
    }
}