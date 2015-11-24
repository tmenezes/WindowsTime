using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace WindowsTime.Infraestrutura.DAO.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetByProperty(string property, object value);
        IEnumerable<T> GetAll();

        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> LinqQuery();
        IQueryable<TOther> LinqQueryFor<TOther>();


        void CreateNew(T entity);
        void Save(T entity);
        void SaveAndPersist(T entity);
        void SaveAll(IEnumerable<T> entityList);
        void Delete(T entity);

        object ExecuteStoredProcedure(string procedureName, params DbParameter[] parameters);
        IEnumerable<T> ExecuteStoredProcedure<T2>(Converter<IDataReader, T2> conversor, string procedureName, params DbParameter[] parameters);
    }
}
