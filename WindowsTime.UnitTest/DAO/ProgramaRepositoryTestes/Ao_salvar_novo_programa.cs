using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;

namespace WindowsTime.UnitTest.DAO.ProgramaRepositoryTestes
{
    [TestClass]
    public class Ao_salvar_novo_programa : AbstractDaoTesteAutoAct
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
            var programa = StubHelper.NovoPrograma();
            var programaRepository = new ProgramaRepository();

            programaRepository.Save(programa);
        }
    }
}