using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace WindowsTime.Infraestrutura.DAO.Repository
{
    public class RepositoryMediator<T> where T : class
    {
        private IRepository<T> _repositoryInterno = null;
        public IRepository<T> RepositoryInterno
        {
            private get { return _repositoryInterno; }
            set { _repositoryInterno = value; }
        }


        public RepositoryMediator()
            : this(new NHibernateRepository<T>())
        {
        }
        public RepositoryMediator(IRepository<T> repositoryImplementation)
        {
            _repositoryInterno = repositoryImplementation;
        }


        public T GetById(object id)
        {
            return RepositoryInterno.GetById(id);
        }
        public IEnumerable<T> GetByProperty(string property, object value)
        {
            return RepositoryInterno.GetByProperty(property, value);
        }
        public IEnumerable<T> GetAll()
        {
            return RepositoryInterno.GetAll();
        }

        public int Count()
        {
            return RepositoryInterno.Count();
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return RepositoryInterno.Count(predicate);
        }
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return RepositoryInterno.FindAll(predicate);
        }
        public IQueryable<T> LinqQuery()
        {
            return RepositoryInterno.LinqQuery();
        }
        public IQueryable<TOther> LinqQueryFor<TOther>()
        {
            return RepositoryInterno.LinqQueryFor<TOther>();
        }

        public void CreateNew(T entity)
        {
            RepositoryInterno.CreateNew(entity);
        }
        public void Save(T entity)
        {
            RepositoryInterno.Save(entity);
        }
        public void SalvarEPersistir(T entity)
        {
            RepositoryInterno.SaveAndPersist(entity);
        }
        public void SavarTodos(IEnumerable<T> entityList)
        {
            RepositoryInterno.SaveAll(entityList);
        }
        public void Excluir(T entity)
        {
            RepositoryInterno.Delete(entity);
        }

        public object ExecuteStoredProcedure(string procedureName, params DbParameter[] parameters)
        {
            return null;
        }
        public IEnumerable<T> ExecuteStoredProcedure<T2>(Converter<IDataReader, T2> conversor, string procedureName, params DbParameter[] parameters)
        {
            return null;
        }
    }
}
