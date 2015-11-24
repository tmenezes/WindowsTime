namespace WindowsTime.Dominio.Repository
{
    public interface ITempoRepository
    {
        UtilizacaoDePrograma ObterUtilizacaoDeProgramasDoDia(Usuario usuario);
        void Salvar(UtilizacaoDePrograma utilizacaoDePrograma);
    }
}