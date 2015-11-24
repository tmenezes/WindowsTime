using System;

namespace WindowsTime.Dominio
{
    public class UtilizacaoDePrograma
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }

        //public IEnumerable<Programa> Programas { get; set; }

        //public UtilizacaoDePrograma()
        //{
        //}
        //public UtilizacaoDePrograma(IEnumerable<DadosDoPrograma> programas)
        //{
        //    Usuario = Usuario.Corrente;
        //    Data = DateTime.Now.Date;
        //    Programas = programas;
        //}
    }
}