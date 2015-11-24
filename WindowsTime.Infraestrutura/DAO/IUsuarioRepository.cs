using WindowsTime.Core.Dominio;

namespace WindowsTime.Infraestrutura.DAO
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuario(string email);
    }
}