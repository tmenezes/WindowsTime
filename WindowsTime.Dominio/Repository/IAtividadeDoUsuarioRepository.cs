namespace WindowsTime.Dominio.Repository
{
    public interface IAtividadeDoUsuarioRepository
    {
        AtividadeDoUsuario ObterAtividadeDoUsuarioDoDia(Usuario usuario);
        void Salvar(AtividadeDoUsuario atividadeDoUsuario);
    }
}