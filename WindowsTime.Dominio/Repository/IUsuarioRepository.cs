namespace WindowsTime.Dominio.Repository
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuario(string email);
    }
}