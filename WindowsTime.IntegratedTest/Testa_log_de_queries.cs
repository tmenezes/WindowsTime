using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.Infraestrutura.DAO;

namespace WindowsTime.IntegratedTest
{
    [TestClass]
    public class Testa_log_de_queries
    {
        public Testa_log_de_queries()
        {
            //var log1 = LogManager.GetLogger("NHibernate");
            //var log2 = LogManager.GetLogger("NHibernate.SQL");
            //var log3 = LogManager.GetLogger("NHibernate.Loader");

            //log1.Info("1 Application starting");
            //log2.Info("2 Application starting");
            //log3.Info("3 Application starting");
        }

        [TestMethod]
        public void Deve_rodar_query()
        {
            using (var session = DataBase.SessionFactoryHolder.OpenSession())
            {
                var fileImport = session.Get<FileImport>(1);
                Assert.IsNotNull(fileImport);
            }
        }
    }
}
