using System;
using System.Drawing;

namespace WindowsTime
{
    internal class DadosDoPrograma
    {
        public string Nome { get; set; }
        public double TempoDeUtilizacao { get; set; }
        public int TotalJanelas { get; set; }
        public Image Icone { get; set; }

        public string TempoFormatado { get { return TimeSpan.FromSeconds(TempoDeUtilizacao).ToString(@"hh\:mm\:ss"); } }
    }
}