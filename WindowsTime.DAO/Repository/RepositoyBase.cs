using System.Collections.Generic;

namespace WindowsTime.DAO.Repository
{
    public class RepositoyBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryMediator<T> RepositoryMediator { get; set; }

        public RepositoyBase()
        {
            RepositoryMediator = new RepositoryMediator<T>();
        }
        
        public RepositoyBase(RepositoryMediator<T> repositoryMediator)
        {
            RepositoryMediator = repositoryMediator;
        }


        //[LogAspect]
        public T GetById(object id)
        {
            return RepositoryMediator.GetById(id);
        }
        //[LogAspect]
        public IEnumerable<T> GetAll()
        {
            return RepositoryMediator.GetAll();
        }

        //[LogAspect]
        public virtual void CreateNew(T entity)
        {
            RepositoryMediator.CreateNew(entity);
        }
        //[LogAspect]
        public void Save(T entity)
        {
            RepositoryMediator.Save(entity);
        }
        //[LogAspect]
        public void SaveAndPersist(T entity)
        {
            RepositoryMediator.SalvarEPersistir(entity);
        }
        //[LogAspect]
        public void SaveAll(IEnumerable<T> entityList)
        {
            RepositoryMediator.SavarTodos(entityList);
        }
        //[LogAspect]
        public void Delete(T entity)
        {
            RepositoryMediator.Excluir(entity);
        }
    }
}
