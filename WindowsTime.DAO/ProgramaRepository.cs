using System.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.Infraestrutura.DAO.Repository;

namespace WindowsTime.DAO
{
    public class ProgramaRepository : RepositoryBase<Programa>, IProgramaRepository
    {
        public Programa ObterProgramaPorNome(string nome)
        {
            return RepositoryMediator.LinqQuery().FirstOrDefault(p => p.Nome == nome);
        }
    }
}