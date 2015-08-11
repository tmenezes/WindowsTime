using System;
using System.Diagnostics;
using System.Drawing;
using WindowsTime.Properties;

namespace WindowsTime
{
    internal class JanelaPorExecutavel
    {
        private Image _icone;

        public string Executavel { get; set; }
        public Process Processo { get; set; }
        public double Tempo { get; set; }
        public int TotalJanelas { get; set; }
        public Image Icone { get { return _icone ?? (_icone = (WindowsApi.GetProcessIcon(Processo) ?? Resources.windows).ToBitmap()); } }

        public string TempoFormatado { get { return TimeSpan.FromSeconds(Tempo).ToString(@"mm\:ss"); } }
    }
}