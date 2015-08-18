using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsTime
{
    public partial class FrmDebug : Form
    {
        private readonly MedidorDeTempoDeJanela _medidor = MedidorDeTempoDeJanela.Instance;
        private int travadasDoPC = 0;

        public FrmDebug()
        {
            InitializeComponent();

            _medidor.TempoMedido += Medidor_TempoMedido;

            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        private void FrmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }


        private void Medidor_TempoMedido(object sender, Janela janela)
        {
            this.Invoke((Action)(() =>
            {
                lblHandle.Text = janela.WindowsHandle.ToString();
                lblIdProcesso.Text = janela.Programa.Processo.Id.ToString();
                lblNomeJanela.Text = janela.Titulo;
                lblExecutavel.Text = janela.Programa.Executavel;
                lblTempo.Text = janela.TempoDeAtividade.ToString(@"mm\:ss");

                DrawProcessIcon(janela);
            }));
        }

        private void DrawProcessIcon(Janela janela)
        {
            try
            {
                var ico = WindowsApi.GetProcessIcon(janela.Programa.Processo);

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
    }
}
