using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsTime.Core.ImportExport.CsvFile;

namespace WindowsTime.Core.Dados
{
    public class DadosDoPrograma
    {
        public int Id { get; set; }

        [CsvFileConfiguration("Programa", Position = 1)]
        public string Nome { get; set; }

        [CsvFileConfiguration("Tempo (em seg.)", Position = 2, HasDecimalDelimiter = true, DecimalPlaces = 2)]
        public double TempoDeUtilizacao { get; set; }

        [CsvFileConfiguration("Janelas", Position = 4)]
        public int TotalJanelas { get; set; }

        public IEnumerable<DadosDaJanela> Janelas { get; set; }

        public Image Icone { get; set; }

        [CsvFileConfiguration("Tempo Formatado", Position = 3)]
        public string TempoFormatado { get { return TimeSpan.FromSeconds(TempoDeUtilizacao).ToString(@"hh\:mm\:ss"); } }
    }
}