using WindowsTime.Core.Monitorador;

namespace WindowsTime.Core.Dados
{
    public class DadosDaJanela
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double TempoDeUtilizacaoTotal { get; set; }
        public double TempoNaoSincronizado { get; set; }

        public DadosDaJanela()
        {
        }
        public DadosDaJanela(Janela janela)
        {
            Titulo = janela.Titulo;
            TempoDeUtilizacaoTotal = janela.TempoDeAtividadeTotal.TotalSeconds;
            TempoNaoSincronizado = janela.TempoNaoSincronizado.TotalSeconds;
        }
    }
}