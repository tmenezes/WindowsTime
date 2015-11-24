using System.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.Infraestrutura.DAO.Repository;

namespace WindowsTime.DAO
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario ObterUsuario(string email)
        {
            return RepositoryMediator.LinqQuery().First(u => u.Email == email);
        }
    }
}