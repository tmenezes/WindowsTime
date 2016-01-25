using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.DAO;
using WindowsTime.Infraestrutura.DAO;

namespace WindowsTime.IntegratedTest.DAO.ProgramaRepositoryTestes
{
    [TestClass]
    public class Ao_obter_programa_existente
    {
        [TestMethod]
        public void Deve_obter_com_sucesso()
        {
            var programa = new ProgramaRepository().ObterProgramaPorNome("Visual Studio 2050");

            Assert.IsNotNull(programa);
            Assert.AreEqual<int>(1, programa.Id);
            Assert.AreEqual<string>("Visual Studio 2050", programa.Nome);
        }

        [TestMethod]
        public void Deve_obter_nada()
        {
            var programa = new ProgramaRepository().ObterProgramaPorNome("xxx");

            Assert.IsNull(programa);
        }

        [TestMethod]
        public void Deve_Atualizar()
        {
            using (var session = DataBase.SessionFactoryHolder.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var programa = session.Query<Programa>().FirstOrDefault(p => p.Nome == "Visual Studio 2050");
                programa.Nome = "Visual Studio (alterado)";
                session.Update(programa);

                tx.Commit();
            }
        }

        [TestMethod]
        public void Deve_Deletar()
        {
            using (var session = DataBase.SessionFactoryHolder.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var programa = session.Query<Programa>().FirstOrDefault(p => p.Nome == "Visual Studio 2050");
                session.Delete(programa);

                tx.Commit();
            }
        }

        [TestMethod]
        public void Threads_devem_obter_com_sucesso()
        {
            Programa programa1 = null;
            Programa programa2 = null;

            var task1 = Task.Factory.StartNew(() => programa1 = new ProgramaRepository().ObterProgramaPorNome("Visual Studio 2050"));
            var task2 = Task.Factory.StartNew(() => programa2 = new ProgramaRepository().ObterProgramaPorNome("Visual Studio 2050"));

            Task.WaitAll(task1, task2);

            Assert.IsNotNull(programa1);
            Assert.AreEqual<int>(1, programa1.Id);
            Assert.AreEqual<string>("Visual Studio 2050", programa1.Nome);
        }

        [TestMethod]
        public void FuturesQueries_devem_obter_com_sucesso()
        {
            using (var session = DataBase.SessionFactoryHolder.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var programa1 = session.Query<Programa>().ToFuture().FirstOrDefault(p => p.Nome == "Visual Studio 2050");
                var programa2 = session.Query<Programa>().ToFuture().FirstOrDefault(p => p.Nome == "Visual Studio 2051");


                Assert.IsNotNull(programa1);
                Assert.IsNotNull(programa1.Nome);

                Assert.IsNull(programa2);

                tx.Commit();
            }
        }
    }
}