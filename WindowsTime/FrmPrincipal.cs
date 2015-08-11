using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsTime
{
    public partial class FrmPrincipal : Form
    {
        private const string TRAY_ABRIR = "1";
        private const string TRAY_PREVIEW = "2";
        private const string TRAY_SAIR = "3";
        private readonly MedidorDeTempoDeJanela _medidor = MedidorDeTempoDeJanela.Instance;
        private bool _jaExibiuPrimeiroBallon = false;
        private int _debugClicks = 0;


        public FrmPrincipal()
        {
            InitializeComponent();

            _medidor.Iniciar();

            timer1.Start();
        }


        // eventos form
        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                MinimizarParaSysTray();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void FrmPrincipal_Activated(object sender, EventArgs e)
        {
            AtualizarTela();
            timer1.Enabled = true;
        }

        private void FrmPrincipal_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                MinimizarParaSysTray();
                e.Cancel = true;
            }
        }

        private void gridProgramas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                AtualizarTela();
            }));
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag.ToString() == TRAY_ABRIR)
            {
                this.Show();
                return;
            }

            if (e.ClickedItem.Tag.ToString() == TRAY_PREVIEW)
            {
                this.Show();
                return;
            }

            if (e.ClickedItem.Tag.ToString() == TRAY_SAIR)
            {
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _debugClicks++;

            if (_debugClicks >= 4)
            {
                new FrmDebug().Show();
                _debugClicks = 0;
            }
        }


        // privados
        private void AtualizarTela()
        {
            this.Cursor = Cursors.WaitCursor;

            var janelas = GetJanelasAgrupadasPorExecutavel();

            DesenharGrafico(janelas);
            CarregarGrid(janelas);

            this.Cursor = Cursors.Default;
        }

        private void DesenharGrafico(IEnumerable<JanelaPorExecutavel> janelas)
        {
            var pontos = chart1.Series[0].Points;
            pontos.Clear();

            foreach (var janela in janelas)
            {
                var ponto = new DataPoint(0, janela.Tempo)
                {
                    LegendText = janela.Executavel
                };
                pontos.Add(ponto);
            }
        }

        private void CarregarGrid(IEnumerable<JanelaPorExecutavel> janelas)
        {
            var source = new BindingSource { DataSource = janelas };

            gridProgramas.AutoGenerateColumns = false;
            gridProgramas.DataSource = source;
        }

        private IEnumerable<JanelaPorExecutavel> GetJanelasAgrupadasPorExecutavel()
        {
            var janelas = _medidor.Janelas.Values
                                  .GroupBy(j => j.NomeDoExecutavel)
                                  .Select(group => new JanelaPorExecutavel()
                                  {
                                      Executavel = group.Key,
                                      Processo = group.First().Processo,
                                      Tempo = group.Sum(i => i.TempoDeAtividade.TotalSeconds),
                                      TotalJanelas = group.Sum(i => i.AreaOuAbasVisitadas),
                                  })
                                  .OrderByDescending(i => i.Tempo)
                                  .ToList();
            return janelas;
        }

        private void MinimizarParaSysTray()
        {
            notifyIcon1.Visible = true;
            this.Hide();

            if (!_jaExibiuPrimeiroBallon)
                notifyIcon1.ShowBalloonTip(3000);

            _jaExibiuPrimeiroBallon = true;
        }
    }
}
