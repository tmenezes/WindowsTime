namespace WindowsTime.Core.Dominio
{
    public class Janela
    {
        public int Id { get; set; }
        public AtividadeDoUsuario Atividade { get; set; }
        public string Titulo { get; set; }
        public Programa Programa { get; set; }
        public double TempoDeAtividade { get; set; }

        public Janela()
        {
        }

        public Janela(AtividadeDoUsuario atividade, string titulo, Programa programa, double tempoDeAtividade)
        {
            Atividade = atividade;
            Titulo = titulo;
            Programa = programa;
            TempoDeAtividade = tempoDeAtividade;
        }
    }
}
