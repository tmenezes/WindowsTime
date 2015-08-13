﻿using System;
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
        private bool _exibiuBallonFechar = false;
        private bool _exibiuBallonMinimizar = false;
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
            if (WindowState == FormWindowState.Minimized)
            {
                MinimizarParaSysTray(fechando: false);
            }
        }

        private void FrmPrincipal_Activated(object sender, EventArgs e)
        {
            AtualizarTela();
            timer1.Enabled = true;
            this.Opacity = 1;
        }

        private void FrmPrincipal_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            if (this.chkSempreVisivel.Checked)
                this.Opacity = 0.75;
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                MinimizarParaSysTray(fechando: true);
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
            ShowForm();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag.ToString() == TRAY_ABRIR)
            {
                ShowForm();
                return;
            }

            if (e.ClickedItem.Tag.ToString() == TRAY_PREVIEW)
            {
                ShowForm();
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

            var janelas = GraficoHelper.GetJanelasAgrupadasPorExecutavel();

            DesenharGrafico(janelas);
            CarregarGrid(janelas);

            this.Cursor = Cursors.Default;
        }

        private void DesenharGrafico(IEnumerable<JanelaPorExecutavel> janelas)
        {
            try
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
            catch (Exception)
            {
                Text = string.Format("Erro: DesenharGrafico - {0}", DateTime.Now);
            }
        }

        private void CarregarGrid(IEnumerable<JanelaPorExecutavel> janelas)
        {
            try
            {
                var linhaSelecionada = (gridProgramas.SelectedRows.Count > 0)
                        ? gridProgramas.SelectedRows[0].Index
                        : -1;

                var source = new BindingSource { DataSource = janelas };
                gridProgramas.AutoGenerateColumns = false;
                gridProgramas.DataSource = source;

                var deveReselecionarLinha = linhaSelecionada >= 0;
                if (deveReselecionarLinha)
                {
                    gridProgramas.SelectedRows.OfType<DataGridViewRow>().ToList().ForEach(r => r.Selected = false);

                    gridProgramas.Rows[linhaSelecionada].Selected = true;
                    gridProgramas.FirstDisplayedScrollingRowIndex = linhaSelecionada;
                }
            }
            catch (Exception)
            {
                Text = string.Format("Erro: CarregarGrid - {0}", DateTime.Now);
            }
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void MinimizarParaSysTray(bool fechando)
        {
            notifyIcon1.Visible = true;
            this.Hide();

            if (fechando)
            {
                if (!_exibiuBallonFechar)
                    notifyIcon1.ShowBalloonTip(3000);

                _exibiuBallonFechar = true;
            }
            else
            {
                if (!_exibiuBallonMinimizar)
                    notifyIcon1.ShowBalloonTip(3000);

                _exibiuBallonMinimizar = true;
            }
        }

        private void chkSempreVisivel_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chkSempreVisivel.Checked;
        }

        private void gridProgramas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var janela = gridProgramas.Rows[e.RowIndex].DataBoundItem as JanelaPorExecutavel;

            if (janela == null)
                return;

            var frmDetalhe = new FrmDetalhe(janela.Executavel);
            frmDetalhe.ShowDialog();
        }
    }
}
