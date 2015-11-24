using WindowsTime.Core.Monitorador;

namespace WindowsTime.Core.DTO
{
    public class JanelaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double TempoDeUtilizacaoTotal { get; set; }
        public double TempoNaoSincronizado { get; set; }

        public JanelaDTO()
        {
        }
        public JanelaDTO(Janela janela)
        {
            Titulo = janela.Titulo;
            TempoDeUtilizacaoTotal = janela.TempoDeAtividadeTotal.TotalSeconds;
            TempoNaoSincronizado = janela.TempoNaoSincronizado.TotalSeconds;
        }
    }
}