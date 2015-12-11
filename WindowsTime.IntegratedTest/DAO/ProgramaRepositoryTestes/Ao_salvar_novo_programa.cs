using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;
using WindowsTime.UnitTest;

namespace WindowsTime.IntegratedTest.DAO.ProgramaRepositoryTestes
{
    [TestClass]
    public class Ao_salvar_novo_programa
    {
        [TestMethod]
        public void Deve_salvar_programa_com_sucesso()
        {
            var programa = StubHelper.NovoPrograma();
            var programaRepository = new ProgramaRepository();

            programaRepository.Save(programa);
        }
    }
}