using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsTime.Core.ImportExport;
using WindowsTime.Core.Monitorador;

namespace WindowsTime
{
    public partial class FrmPrincipal : Form
    {
        private const string TRAY_ABRIR = "1";
        private const string TRAY_PREVIEW = "2";
        private const string TRAY_SAIR = "3";
        private const int GRID_TOTAL_LINHAS_VISIVEIS = 6;
        private bool _exibiuBallonFechar = false;
        private bool _exibiuBallonMinimizar = false;
        private int _debugClicks = 0;


        public FrmPrincipal()
        {
            InitializeComponent();

            timer1.Start();

            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
#if !DEBUG
            if (e.CloseReason == CloseReason.UserClosing)
            {
                MinimizarParaSysTray(fechando: true);
                e.Cancel = true;
            }
#endif
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

        private void chkSempreVisivel_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chkSempreVisivel.Checked;
        }

        private void gridProgramas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var programa = gridProgramas.Rows[e.RowIndex].DataBoundItem as DadosDoPrograma;

            if (programa == null)
                return;

            var frmDetalhe = new FrmDetalhe(programa.Nome);
            frmDetalhe.ShowDialog();
        }

        private void picExportar_Click(object sender, EventArgs e)
        {
            try
            {
                var result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;


                var programas = GraficoHelper.GetProgramas();
                var arquivo = saveFileDialog1.FileName;

                var exportador = ExportFile.GetExporter(ImportExportTypeEnum.CsvFile, arquivo);
                exportador.DoExport(programas);

                MessageBox.Show("Dados exportados com sucesso!.", "Salvar dados...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao exportar dados. Verifique se o arquivo destino não está em uso e tente novamente.",
                                "Salvar dados...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // privados
        private void AtualizarTela()
        {
            this.Cursor = Cursors.WaitCursor;

            var programas = GraficoHelper.GetProgramas();

            DesenharGrafico(programas);
            CarregarGrid(programas);

            this.Cursor = Cursors.Default;
        }

        private void DesenharGrafico(IEnumerable<DadosDoPrograma> programas)
        {
            try
            {
                var pontos = chart1.Series[0].Points;
                pontos.Clear();

                foreach (var programa in programas)
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

        private void CarregarGrid(IEnumerable<DadosDoPrograma> programas)
        {
            try
            {
                var linhaSelecionada = (gridProgramas.SelectedRows.Count > 0)
                        ? gridProgramas.SelectedRows[0].Index
                        : -1;

                var source = new BindingSource { DataSource = programas };
                gridProgramas.AutoGenerateColumns = false;
                gridProgramas.DataSource = source;

                var deveReselecionarLinha = linhaSelecionada >= 0;
                if (deveReselecionarLinha)
                {
                    gridProgramas.SelectedRows.OfType<DataGridViewRow>().ToList().ForEach(r => r.Selected = false);

                    gridProgramas.Rows[linhaSelecionada].Selected = true;
                    gridProgramas.FirstDisplayedScrollingRowIndex = ((linhaSelecionada + 1) > GRID_TOTAL_LINHAS_VISIVEIS) ? linhaSelecionada - GRID_TOTAL_LINHAS_VISIVEIS + 1 : 0;
                }
            }
            catch (Exception ex)
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

        private void button1_Click(object sender, EventArgs e)
        {
            #region windows store package pelo PID
            //var idProcesso = Interaction.InputBox("Digite o id do processo ou handle da janela", "Id do Process", "", -1, -1);

            //var processo = ProcessHelper.GetProcess(Convert.ToInt32(idProcesso));
            //if (!WindowsApi.IsWindowsStoreApp(processo))
            //{
            //    MessageBox.Show("Não é Windows Store App");
            //    return;
            //}

            //var packageId = WindowsApi.GetWindowsStorePackageId(processo);
            ////var process = WindowsApi.GetWindowsStorere(processo);
            //MessageBox.Show(string.Format("{0} - {1}", 0, packageId)); 
            #endregion

            #region janelas filhas pelo window handle
            //var input = Interaction.InputBox("Digite o handle da janela", "Handle da janela", "", -1, -1);
            //var handle = new IntPtr(Convert.ToInt32(input));
            //var janelasFilhas = WindowsApi.GetChildWindows(handle);

            //if (janelasFilhas.Any())
            //{
            //    janelasFilhas.Insert(0, handle);
            //    var janelas = janelasFilhas.Select(i => new { Handle = i.ToString(), Processo = WindowsApi.GetProcess(i) })
            //                               .Select(i => string.Format("Handle:{0} - PID:{1}, {2}", i.Handle, i.Processo.Id, i.Processo))
            //                               .Aggregate((um, outro) => string.Format("{0}\n{1}", um, outro));
            //    MessageBox.Show(janelas);
            //}
            //else
            //{
            //    MessageBox.Show("Nada");
            //} 
            #endregion


            #region atualizar programas das janelas
            var janelasWindowsStore = MonitoradorDeJanela.Instance.Janelas.Values.Where(j => j.Programa.Tipo == TipoDePrograma.WindowsStore &&
                                                                                             j.Programa.Processo.ProcessName == "ApplicationFrameHost");
            foreach (var janela in janelasWindowsStore)
            {
                janela.AtualizarPrograma();
            }
            #endregion

            #region janelas filhas com verificacao de monitaradas
            //var input = Interaction.InputBox("Digite o handle da janela", "Handle da janela", "", -1, -1);
            //var handle = new IntPtr(Convert.ToInt32(input));

            //bool janelaExisteNoMonitorador = MonitoradorDeJanela.Instance.Janelas.ContainsKey(handle);
            //MessageBox.Show("janelaExisteNoMonitorador: " + janelaExisteNoMonitorador);


            //var childWindows = WindowsApi.GetChildWindows(handle);

            //if (!childWindows.Any())
            //{
            //    MessageBox.Show("nada");
            //    return;
            //}

            //var processes = childWindows.Select(i => WindowsApi.GetProcess(i))
            //                            .GroupBy(p => p.Id)
            //                            .Select(group => group.First())
            //                            .Select(i => string.Format("PID:{0}, {1}", i.Id, i.ProcessName))
            //                            .Aggregate((um, outro) => string.Format("{0}\n{1}", um, outro));
            //MessageBox.Show(processes); 
            #endregion
        }
    }
}
