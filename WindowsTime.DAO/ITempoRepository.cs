using WindowsTime.Core.Dados;

namespace WindowsTime.DAO
{
    public interface ITempoRepository
    {
        UtilizacaoDePrograma ObterUtilizacaoDeProgramasDoDia(Usuario usuario);
        void Salvar(UtilizacaoDePrograma utilizacaoDePrograma);
    }
}