using System;
using System.Configuration;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using WindowsTime.Infraestrutura.DAO.Map;

namespace WindowsTime.Infraestrutura.DAO.Infra
{
    public class NHibernateHelper
    {
        // atributos
        private static volatile ISessionFactory _sessionFactory = null;
        private static readonly object _syncObject = new object();

        // publicos
        //[LogAspect(LogTypeEnum.Debug)]
        public static void InitializarSessionFactory()
        {
            if (_sessionFactory != null)
                return;

            var persistenceModel = GetPersistenceModel();

            var config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromConnectionStringWithKey("DRIVER_FREAKOM")).ShowSql())
                .Mappings(m => m.UsePersistenceModel(persistenceModel)
                                   .FluentMappings.AddFromAssemblyOf<UsuarioMap>()
                                   .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()));

            _sessionFactory = config.BuildSessionFactory();
        }

        //[LogAspect(LogTypeEnum.Debug)]
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }


        // privado
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null) return _sessionFactory;

                lock (_syncObject)
                {
                    if (_sessionFactory == null)
                        InitializarSessionFactory();
                }

                return _sessionFactory;
            }
        }

        private static PersistenceModel GetPersistenceModel()
        {
            var persistenceModel = new PersistenceModel();

            var path = ConfigurationManager.AppSettings["HibernateExportPath"];

            if (!String.IsNullOrEmpty(path))
                persistenceModel.WriteMappingsTo(path);

            return persistenceModel;
        }
    }
}
