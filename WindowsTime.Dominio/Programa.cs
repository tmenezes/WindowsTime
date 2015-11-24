namespace WindowsTime.Dominio
{
    public class Programa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Programa()
        {

        }

        public Programa(string nome)
        {
            Nome = nome;
        }
    }
}