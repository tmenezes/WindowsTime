using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using WindowsTime.DAO.Map;
using WindowsTime.Infraestrutura.DAO;
using WindowsTime.Infraestrutura.Logging.Log4NetAppender;

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
                //return @"Data Source=192.168.2.83\SQLDEV;Database=iMusicaClosing;User ID=SA;Password=iMusicaBackOffice@123;";
            }
        }
        private static string MasterDbConnectionString
        {
            get { return $"server=(localdb)\\{_instanceName};Trusted_Connection=yes;database=master;Integrated Security=true;connection timeout=30"; }
        }


        public void InitializeSessionFactory()
        {
            if (_sessionFactory != null)
                return;

            var config = Fluently.Configure()
                                 .Database(MsSqlConfiguration.MsSql2008.ConnectionString(WorkingDbConnectionString).ShowSql().FormatSql())
                                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMap>()
                                                 .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never())
                                                 .Conventions.Add())
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

                //XmlConfigurator.Configure();
                ConfigureLogger();
                DataBase.Inicializar(new DaoTesteBase());

                SetupDatabase();
            }
            catch (Exception)
            {
                TestCleanup();
                throw;
            }
        }

        [AssemblyCleanup]
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
            var programaPadrao1 = "INSERT INTO Programa (Nome) VALUES ('Visual Studio 2050')";
            var programaPadrao2 = "INSERT INTO Programa (Nome) VALUES ('Windows Explorer')";
            var atividadePadrao = $"INSERT INTO Atividade (IdUsuario, Data) VALUES (1, '{DateTime.Now:yyyy-MM-dd}');";
            var janelaPadrao1 = $"INSERT INTO Janela (IdAtividade, IdPrograma, Titulo, TempoDeAtividade) VALUES (1, 1, 'VS 2015', 5.250);";
            var janelaPadrao2 = $"INSERT INTO Janela (IdAtividade, IdPrograma, Titulo, TempoDeAtividade) VALUES (1, 2, 'Explorer', 2.100);";

            RunSqlCommands(WorkingDbConnectionString, new[]
                                                      {
                                                          usuarioPadrao,
                                                          programaPadrao1,
                                                          programaPadrao2,
                                                          atividadePadrao,
                                                          janelaPadrao1,
                                                          janelaPadrao2,
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

        private static void ConfigureLogger()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var layout = new PatternLayout();
            layout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            layout.ActivateOptions();

            var traceAppender = new TraceAppender();
            traceAppender.Layout = layout;
            traceAppender.ActivateOptions();

            var fileAppender = new SimpleFileAppender();
            fileAppender.Layout = layout;
            fileAppender.ActivateOptions();

            //var nhLoadLogger = (Logger)LogManager.GetLogger("NHibernate.Loader").Logger;
            //nhLoadLogger.Level = Level.All;
            //nhLoadLogger.AddAppender(fileAppender);
            //nhLoadLogger.Additivity = false;


            BasicConfigurator.Configure(traceAppender);
            //BasicConfigurator.Configure(fileAppender);

            //hierarchy.Root.RemoveAppender(fileAppender);
        }
    }
}
