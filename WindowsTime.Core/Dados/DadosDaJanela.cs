namespace WindowsTime.Core.Dados
{
    public class DadosDaJanela
    {
        public int Id { get; set; }
        public DadosDoPrograma Programa { get; set; }
        public string Titulo { get; set; }
        public double TempoDeUtilizacao { get; set; }
    }
}