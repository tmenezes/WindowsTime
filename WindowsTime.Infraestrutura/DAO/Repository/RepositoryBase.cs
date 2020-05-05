using System.Collections.Generic;

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

        
        public T GetById(object id)
        {
            return RepositoryMediator.GetById(id);
        }
        public IEnumerable<T> GetAll()
        {
            return RepositoryMediator.GetAll();
        }

        public virtual void CreateNew(T entity)
        {
            RepositoryMediator.CreateNew(entity);
        }
        public void Save(T entity)
        {
            RepositoryMediator.Save(entity);
        }
        public void SaveAndPersist(T entity)
        {
            RepositoryMediator.SalvarEPersistir(entity);
        }
        public void SaveAll(IEnumerable<T> entityList)
        {
            RepositoryMediator.SavarTodos(entityList);
        }
        public void Delete(T entity)
        {
            RepositoryMediator.Excluir(entity);
        }
    }
}
