using System.Collections.Generic;
using WindowsTime.Infraestrutura.Aop;

namespace WindowsTime.Infraestrutura.DAO.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryMediator<T> RepositoryMediator { get; set; }

        public RepositoryBase()
        {
            RepositoryMediator = new RepositoryMediator<T>();
        }
        
        public RepositoryBase(RepositoryMediator<T> repositoryMediator)
        {
            RepositoryMediator = repositoryMediator;
        }


        [LogAspect]
        public T GetById(object id)
        {
            return RepositoryMediator.GetById(id);
        }
        [LogAspect]
        public IEnumerable<T> GetAll()
        {
            return RepositoryMediator.GetAll();
        }

        [LogAspect]
        public virtual void CreateNew(T entity)
        {
            RepositoryMediator.CreateNew(entity);
        }
        [LogAspect]
        public void Save(T entity)
        {
            RepositoryMediator.Save(entity);
        }
        [LogAspect]
        public void SaveAndPersist(T entity)
        {
            RepositoryMediator.SalvarEPersistir(entity);
        }
        [LogAspect]
        public void SaveAll(IEnumerable<T> entityList)
        {
            RepositoryMediator.SavarTodos(entityList);
        }
        [LogAspect]
        public void Delete(T entity)
        {
            RepositoryMediator.Excluir(entity);
        }
    }
}
