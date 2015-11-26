using System.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.Infraestrutura.DAO.Repository;

namespace WindowsTime.DAO
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario ObterUsuarioPorEmail(string email)
        {
            return RepositoryMediator.LinqQuery().FirstOrDefault(u => u.Email == email);
        }
    }
}