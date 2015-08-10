using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsTime
{
    public partial class FrmPrincipal : Form
    {
        private readonly MedidorDeTempoDeJanela _medidor = MedidorDeTempoDeJanela.Instance;

        public FrmPrincipal()
        {
            InitializeComponent();

            DesenharGrafico();
        }

        private void DesenharGrafico()
        {
            var pontos = chart1.Series[0].Points;
            pontos.Clear();

            var janelas = GetJanelasAgrupadasPorExecutavel();
            foreach (var janela in janelas)
            {
                var ponto = new DataPoint(0, janela.Tempo)
                {
                    LegendText = janela.Executavel
                };
                pontos.Add(ponto);
            }
        }

        private IEnumerable<JanelaPorExecutavel> GetJanelasAgrupadasPorExecutavel()
        {
            var janelas = _medidor.Janelas.Values
                                  .GroupBy(j => j.NomeDoExecutavel)
                                  .Select(group => new JanelaPorExecutavel(group.Key, group.Sum(i => i.TempoDeAtividade.TotalSeconds)))
                                  .ToList();
            return janelas;
        }

        internal class JanelaPorExecutavel
        {
            public string Executavel { get; set; }
            public double Tempo { get; set; }

            public JanelaPorExecutavel(string executavel, double tempo)
            {
                Executavel = executavel;
                Tempo = tempo;
            }
        }
    }
}
