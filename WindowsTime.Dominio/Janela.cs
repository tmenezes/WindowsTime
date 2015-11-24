namespace WindowsTime.Dominio
{
    public class Janela
    {
        public int Id { get; set; }
        public string Titulo { get; private set; }
        public Programa Programa { get; private set; }
        public double TempoDeUtilizacao { get; set; }

        public Janela()
        {
        }

        public Janela(string titulo, Programa programa, double tempoDeUtilizacao)
        {
            Titulo = titulo;
            Programa = programa;
            TempoDeUtilizacao = tempoDeUtilizacao;
        }
    }
}
