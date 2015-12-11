using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;

namespace WindowsTime.IntegratedTest.DAO.Relatorios.RelatoriosGeraisTestes
{
    [TestClass]
    public class Ao_obter_relatorios_de_programas_mais_utilizados
    {
        [TestMethod]
        public void Deve_retornar_com_sucesso()
        {
            var dao = new RelatoriosGeraisDAO();

            var programas = dao.ObterProgramasMaisUtilizadosDaSemana();

            Assert.IsNotNull(programas);
        }
    }
}
