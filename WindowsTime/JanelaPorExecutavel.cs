using System;
using System.Diagnostics;
using System.Drawing;

namespace WindowsTime
{
    internal class JanelaPorExecutavel
    {
        public string Executavel { get; set; }
        public Process Processo { get; set; }
        public double Tempo { get; set; }
        public int TotalJanelas { get; set; }
        public Image Icone { get; set; }

        public string TempoFormatado { get { return TimeSpan.FromSeconds(Tempo).ToString(@"mm\:ss"); } }
    }
}