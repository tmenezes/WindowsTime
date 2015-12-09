using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using WindowsTime.DAO.Map;
using WindowsTime.Infraestrutura.DAO;

namespace WindowsTime.DAO
{
    public class SessionFactoryHolder : ISessionFactoryHolder
    {
        private static volatile ISessionFactory _sessionFactory = null;

        public void InitializeSessionFactory()
        {
            if (_sessionFactory != null)
                return;

            var config = Fluently.Configure()
                                 .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("WindowsTime")).ShowSql())
                                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMap>()
                                                 .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()));

            _sessionFactory = config.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            if (_sessionFactory == null)
                throw new InvalidOperationException("Session não foi incializada!");

            return _sessionFactory.OpenSession();
        }
    }
}