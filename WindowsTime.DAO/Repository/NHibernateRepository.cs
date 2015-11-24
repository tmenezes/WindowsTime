using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WindowsTime.DAO.Repository
{
    public class NHibernateRepository<T> : IRepository<T> where T : class
    {
        private ISession _session = null;


        private ISession Session
        {
            get
            {
                if (_session != null) return _session;

                _session = GetNewSession();

                return _session;
            }
        }
        private ISession GetNewSession()
        {
            return NHibernateHelper.OpenSession();
        }

        public T GetById(object id)
        {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> GetByProperty(string property, object value)
        {
            return Session.CreateCriteria<T>().Add(Restrictions.Eq(property, value)).List<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return Session.QueryOver<T>().List();
        }

        public int Count()
        {
            return Session.QueryOver<T>().RowCount();
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return LinqQuery().Count(predicate);
        }
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return LinqQuery().Where(predicate);
        }

        public object GetSessionContext()
        {
            return Session;
        }
        public void CloseSessionContext(object sessionContext)
        {
            var session = GarantirISession(sessionContext);

            session.Flush();
            session.Dispose();
        }

        public IQueryable<T> LinqQuery()
        {
            return Session.Query<T>();
        }
        public IQueryable<T> LinqQuery(object sessionContext)
        {
            var session = GarantirISession(sessionContext);
            return session.Query<T>();
        }
        public IQueryable<TOther> LinqQueryFor<TOther>()
        {
            return Session.Query<TOther>();
        }

        public void CreateNew(T entity)
        {
        }

        public void Save(T entity)
        {
            Session.SaveOrUpdate(entity);
        }
        public void SaveAndPersist(T entity)
        {
            using (var session = GetNewSession())
            {
                session.SaveOrUpdate(entity);
                session.Flush();
            }
        }
        public void SaveAll(IEnumerable<T> entityList)
        {
            foreach (var entidade in entityList)
            {
                Save(entidade);
            }
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public IQueryable<T> Include<TInclude>(IQueryable<T> query, Expression<Func<T, TInclude>> include)
        {
            return query.Fetch(include);
        }

        public object ExecuteStoredProcedure(string nomeProcedure, params DbParameter[] parametros)
        {
            return null;
        }
        public IEnumerable<T> ExecuteStoredProcedure<T2>(Converter<IDataReader, T2> conversor, string nomeProcedure, params DbParameter[] parametros)
        {
            return null;
        }


        // privados
        private static ISession GarantirISession(object sessionContext)
        {
            if (!(sessionContext is ISession))
                throw new InvalidOperationException("Argumento 'sessionContext' não é um 'ISession'");

            return (sessionContext as ISession);
        }
    }
}
