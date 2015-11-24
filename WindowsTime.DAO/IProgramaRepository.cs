using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO
{
    public interface IProgramaRepository
    {
        Programa ObterPrograma(string nome);
    }
}