using WindowsTime.Core.Dominio;

namespace WindowsTime.Infraestrutura.DAO
{
    public interface IProgramaRepository
    {
        Programa ObterPrograma(string nome);
    }
}