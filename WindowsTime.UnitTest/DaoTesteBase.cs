using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using WindowsTime.Infraestrutura.DAO;

namespace WindowsTime.UnitTest
{
    [TestClass]
    public class DaoTesteBase : ISessionFactoryHolder
    {
        private static volatile ISessionFactory _sessionFactory = null;

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

        public static void Create(string databaseName, string instanceConnection)
        {
            string databaseDirectory = Directory.GetCurrentDirectory();

            var db = new SqlConnection("server=" + instanceConnection + ";" +
                                       "Trusted_Connection=yes;" +
                                       "database=master; " +
                                       "Integrated Security=true; " +
                                       "connection timeout=30");

            db.Open();

            var myCommand = new SqlCommand(@"CREATE DATABASE [" + databaseName + @"]
                CONTAINMENT = NONE
                ON  PRIMARY 
                ( NAME = N'" + databaseName + @"', FILENAME = N'" + databaseDirectory + @"\" + databaseName +
                                                                @".mdf' , SIZE = 3072KB , FILEGROWTH = 1024KB )
                LOG ON 
                ( NAME = N'" + databaseName + @"_log', FILENAME = N'" + databaseDirectory + @"\" + databaseName +
                                                                @"_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)
                ", db);

            myCommand.ExecuteNonQuery();
            db.Close();

        }


        [AssemblyInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            RunSqlLocalDbComand("/c sqllocaldb create \"testinstance\" -s");

            Create("WindowsTime", "(localdb)\\testinstance");
        }

        [AssemblyCleanup]
        public static void TestCleanup()
        {
            RunSqlLocalDbComand("/c sqllocaldb stop \"testinstance\"");
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
        }
    }
}
