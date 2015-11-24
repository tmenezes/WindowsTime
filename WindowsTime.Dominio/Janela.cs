namespace WindowsTime.Dominio
{
    public class Janela
    {
        public int Id { get; set; }
        public string Titulo { get; private set; }
        public Programa Programa { get; private set; }
        public double TempoDeUtilizacao { get; set; }
    }
}
