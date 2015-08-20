using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsTime
{
    public partial class FrmDetalhe : Form
    {
        public FrmDetalhe(string programaAlvo)
        {
            InitializeComponent();

            AtualizarTela(programaAlvo);
        }


        private void AtualizarTela(string programaAlvo)
        {
            DesenharGrafico(programaAlvo);
            CarregarGrid(programaAlvo);

            lblPrograma.Text = programaAlvo;
            statusBarLabelJanelas.Text = gridProgramas.RowCount.ToString();
        }

        private void DesenharGrafico(string programaAlvo)
        {
            try
            {
                var pontos = chart1.Series[0].Points;
                pontos.Clear();

                var programasDestacandoAlvo = GraficoHelper.GetProgramas(programaAlvo);

                foreach (var programa in programasDestacandoAlvo)
                {
                    var ponto = new DataPoint(0, programa.TempoDeUtilizacao)
                    {
                        LegendText = programa.Nome
                    };
                    pontos.Add(ponto);
                }
            }
            catch (Exception)
            {
                Text = string.Format("Erro: DesenharGrafico - {0}", DateTime.Now);
            }
        }

        private void CarregarGrid(string programaAlvo)
        {
            try
            {
                var janelas = MonitoradorDeJanela.Instance.Janelas
                                                 .Where(j => j.Value.Programa.Nome == programaAlvo).Select(i => i.Value)
                                                 .SelectMany(j => j.Programa.AreasVisitadas)
                                                 .Select(i => new { Nome = i })
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
