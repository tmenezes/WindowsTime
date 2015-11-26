using System;
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

namespace WindowsTime.IntegratedTest
{
    [TestClass]
    public class DaoTesteBase : ISessionFactoryHolder
    {
        private static string _instanceName = "WindowsTimeLocalDb";
        private static string _workingDbName = "WindowsTime";
        private static ISessionFactory _sessionFactory = null;


        private static string WorkingDbConnectionString
        {
            get
            {
                var dbDirectory = Directory.GetCurrentDirectory();
                var connString = $"Server=(localdb)\\{_instanceName};AttachDbFilename={dbDirectory}\\{_workingDbName}.MDF;Initial Catalog={_workingDbName};Integrated Security=True";

                return connString;
            }
        }
        private static string MasterDbConnectionString
        {
            get { return $"server=(localdb)\\{_instanceName};Trusted_Connection=yes;database=master; Integrated Security=true; connection timeout=30"; }
        }


        public void InitializeSessionFactory<T>()
        {
            if (_sessionFactory != null)
                return;

            var config = Fluently.Configure()
                                 .Database(MsSqlConfiguration.MsSql2012.ConnectionString(WorkingDbConnectionString).ShowSql())
                                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>()
                                                 .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                                 .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));

            _sessionFactory = config.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }



        [AssemblyInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            try
            {
                TestCleanup();

                RunSqlLocalDbComand($"/c sqllocaldb create \"{_instanceName}\" -s");
                CreateDatabaseLocalDb(_workingDbName);

                XmlConfigurator.Configure();
                DataBase.Inicializar<UsuarioMap>(new DaoTesteBase());

                SetupDatabase();
            }
            catch (Exception)
            {
                TestCleanup();
                throw;
            }
        }

        //[AssemblyCleanup]
        public static void TestCleanup()
        {
            RunSqlLocalDbComand($"/c sqllocaldb stop \"{_instanceName}\"");
            RunSqlLocalDbComand($"/c sqllocaldb delete \"{_instanceName}\"");

            var dbFile = Path.Combine(Directory.GetCurrentDirectory(), $"{_workingDbName}.mdf");
            var logFile = Path.Combine(Directory.GetCurrentDirectory(), $"{_workingDbName}_log.ldf");
            File.Delete(dbFile); Trace.WriteLine($"delete {dbFile}");
            File.Delete(logFile); Trace.WriteLine($"delete {logFile}");
        }


        private static void SetupDatabase()
        {
            var usuarioPadrao = "INSERT INTO Usuario (Email, Nome, DataDeCadastro) VALUES ('thiagomenezes2k7@gmail.com', 'TMenezes', '2015-01-01');";
            var programaPadrao = "INSERT INTO Programa (Nome) VALUES ('Visual Studio 2050')";

            RunSqlCommands(WorkingDbConnectionString, new[]
                                                      {
                                                          usuarioPadrao,
                                                          programaPadrao
                                                      });
        }

        private static void CreateDatabaseLocalDb(string dbName)
        {
            var dbDirectory = Directory.GetCurrentDirectory();
            var commandText = $@"CREATE DATABASE [{dbName}] CONTAINMENT = NONE
                ON  PRIMARY ( NAME = N'{dbName}', FILENAME = N'{dbDirectory}\{dbName}.mdf' , SIZE = 4096KB , FILEGROWTH = 1024KB )
                LOG ON      ( NAME = N'{dbName}_log', FILENAME = N'{dbDirectory}\{dbName}_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)";

            RunSqlCommand(MasterDbConnectionString, commandText);
        }

        private static void RunSqlCommand(string connString, string commandText)
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(commandText, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Trace.WriteLine($"sql cmd executed: {commandText}");
            }
        }
        private static void RunSqlCommands(string connString, string[] commands)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand("", conn);
                foreach (var cmdText in commands)
                {
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                    Trace.WriteLine($"sql cmd executed: {cmdText}");
                }
                cmd.Dispose();
            }
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
