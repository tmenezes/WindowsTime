using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuario(string email);
    }
}