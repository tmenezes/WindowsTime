using System.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.Infraestrutura.DAO.Repository;

namespace WindowsTime.DAO
{
    public class ProgramaRepository : RepositoryBase<Programa>, IProgramaRepository
    {
        public Programa ObterPrograma(string nome)
        {
            return RepositoryMediator.LinqQuery().First(p => p.Nome == nome);
        }
    }
}