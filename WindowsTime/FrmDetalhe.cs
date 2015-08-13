using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsTime
{
    public partial class FrmDetalhe : Form
    {
        private readonly MedidorDeTempoDeJanela _medidor = MedidorDeTempoDeJanela.Instance;

        public FrmDetalhe(string executavelAlvo)
        {
            InitializeComponent();

            AtualizarTela(executavelAlvo);
        }


        private void AtualizarTela(string executavelAlvo)
        {
            DesenharGrafico(executavelAlvo);
            CarregarGrid(executavelAlvo);
        }

        private void DesenharGrafico(string executavelAlvo)
        {
            try
            {
                var pontos = chart1.Series[0].Points;
                pontos.Clear();

                var janelasDestacandoAlvo = GraficoHelper.GetJanelasAgrupadasPorExecutavel(executavelAlvo);

                foreach (var janela in janelasDestacandoAlvo)
                {
                    var ponto = new DataPoint(0, janela.Tempo)
                    {
                        LegendText = janela.Executavel
                    };
                    pontos.Add(ponto);
                }
            }
            catch (Exception)
            {
                Text = string.Format("Erro: DesenharGrafico - {0}", DateTime.Now);
            }
        }

        private void CarregarGrid(string executavelAlvo)
        {
            try
            {
                var janelas = MedidorDeTempoDeJanela.Instance.Janelas
                                                    .Where(j => j.Value.NomeDoExecutavel == executavelAlvo).Select(i => i.Value)
                                                    .SelectMany(j => j.AbasVisitadas)
                                                    .Select(i => new { Executavel = i })
                                                    .ToList();

                var source = new BindingSource { DataSource = janelas };
                gridProgramas.AutoGenerateColumns = false;
                gridProgramas.DataSource = source;
            }
            catch (Exception)
            {
                Text = string.Format("Erro: CarregarGrid - {0}", DateTime.Now);
            }
        }
    }
}
