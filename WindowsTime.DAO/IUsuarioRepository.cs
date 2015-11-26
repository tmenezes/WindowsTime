using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuarioPorEmail(string email);
        void Save(Usuario usuario);
    }
}