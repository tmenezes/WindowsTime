using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsTime
{
    public partial class FrmDebug : Form
    {
        private readonly MonitoradorDeJanela _monitorador = MonitoradorDeJanela.Instance;
        private int travadasDoPC = 0;
        private string executavelAnterior;
        private string idProcessoAnterior;

        public FrmDebug()
        {
            InitializeComponent();

            _monitorador.TempoMedido += MonitoradorTempoMedido;

            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        private void FrmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        private void MonitoradorTempoMedido(object sender, Janela janela)
        {
            this.Invoke((Action)(() =>
            {
                executavelAnterior = lblExecutavel.Text;
                idProcessoAnterior = lblIdProcesso.Text;

                lblHandle.Text = janela.WindowsHandle.ToString();
                lblIdProcesso.Text = (janela.Programa.Processo != null) ? janela.Programa.Processo.Id.ToString() : "???";
                lblNomeJanela.Text = janela.Titulo;
                lblPrograma.Text = janela.Programa.Nome;
                lblExecutavel.Text = janela.Programa.Executavel;
                lblTempo.Text = janela.TempoDeAtividade.ToString(@"mm\:ss");

                DrawProcessIcon(janela);
            }));
        }

        private void DrawProcessIcon(Janela janela)
        {
            try
            {
                var ico = janela.Programa.Processo.GetIcon();

                picIconeProcesso.Image = ico.ToBitmap();
                picIconeProcesso.Visible = true;
            }
            catch (Exception)
            {
                picIconeProcesso.Visible = false;
            }
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                //I left my desk
                travadasDoPC++;
                lblBloqueouPC.Text = travadasDoPC.ToString();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                //I returned to my desk
            }
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {
            try
            {
                new frmTeste(executavelAnterior).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
