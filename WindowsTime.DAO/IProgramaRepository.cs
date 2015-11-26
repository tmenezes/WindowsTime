using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO
{
    public interface IProgramaRepository
    {
        Programa ObterProgramaPorNome(string nome);
        void Save(Programa programa);
    }
}