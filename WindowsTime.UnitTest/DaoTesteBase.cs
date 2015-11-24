using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using WindowsTime.DAO.Map;
using WindowsTime.Infraestrutura.DAO;

namespace WindowsTime.UnitTest
{
    [TestClass]
    public class DaoTesteBase : ISessionFactoryHolder
    {
        private static ISessionFactory _sessionFactory = null;

        public void InitializeSessionFactory<T>()
        {
            if (_sessionFactory != null)
                return;

            var config = Fluently.Configure()
                                 .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("WindowsTime")).ShowSql())
                                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>()
                                                 .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                                 .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));

            _sessionFactory = config.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return default(ISession);
        }

        public static void CreateDatabaseLocalDb(string databaseName, string instanceConnection)
        {
            string databaseDirectory = Directory.GetCurrentDirectory();

            var db = new SqlConnection("server=" + instanceConnection + ";" +
                                       "Trusted_Connection=yes;" +
                                       "database=master; " +
                                       "Integrated Security=true; " +
                                       "connection timeout=30");

            var myCommand = new SqlCommand(string.Format(@"CREATE DATABASE [{0}]
                CONTAINMENT = NONE
                ON  PRIMARY 
                ( NAME = N'{0}', FILENAME = N'{1}\{0}.mdf' , SIZE = 4096KB , FILEGROWTH = 1024KB )
                LOG ON 
                ( NAME = N'{0}_log', FILENAME = N'{1}\{0}_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)
                ", databaseName, databaseDirectory), db);

            db.Open();
            myCommand.ExecuteNonQuery();
            db.Close();
        }


        [AssemblyInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            RunSqlLocalDbComand("/c sqllocaldb create \"testinstance\" -s");
            CreateDatabaseLocalDb("WindowsTime", "(localdb)\\testinstance");

            XmlConfigurator.Configure();
            DataBase.Inicializar<UsuarioMap>(new DaoTesteBase());
        }

        [AssemblyCleanup]
        public static void TestCleanup()
        {
            RunSqlLocalDbComand("/c sqllocaldb stop \"testinstance\"");
            RunSqlLocalDbComand("/c sqllocaldb delete \"testinstance\"");

            var dbFile = Path.Combine(Directory.GetCurrentDirectory(), "WindowsTime.mdf");
            File.Delete(dbFile);
        }

        private static void RunSqlLocalDbComand(string command)
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = command
            };

            var process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();

            Trace.WriteLine($"{startInfo.FileName} {startInfo.Arguments}");
        }
    }
}
