using WindowsTime.Core.Monitorador;

namespace WindowsTime.Core.Dados
{
    public class DadosDaJanela
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double TempoDeUtilizacao { get; set; }

        public DadosDaJanela()
        {
        }
        public DadosDaJanela(Janela janela)
        {
            Titulo = janela.Titulo;
            TempoDeUtilizacao = janela.TempoDeAtividade.TotalSeconds;
        }
    }
}