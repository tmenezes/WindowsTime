using System.Collections.Generic;

namespace WindowsTime.DAO.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();

        void CreateNew(T entity);
        void Save(T entity);
        void SaveAndPersist(T entity);
        void SaveAll(IEnumerable<T> entityList);
        void Delete(T entity);
    }
}
