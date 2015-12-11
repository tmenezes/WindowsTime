namespace WindowsTime.Core.Dominio
{
    public class Programa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double TempoDeAtividade { get; set; }

        public Programa()
        {

        }

        public Programa(string nome)
        {
            Nome = nome;
        }
    }
}