using WindowsTime.Core.Dominio;

namespace WindowsTime.Infraestrutura.DAO
{
    public interface IAtividadeDoUsuarioRepository
    {
        AtividadeDoUsuario ObterAtividadeDoUsuarioDoDia(Usuario usuario);
        void Salvar(AtividadeDoUsuario atividadeDoUsuario);
    }
}