using System;
using System.Windows.Forms;

namespace WindowsTime
{
    public partial class FrmDebug : Form
    {
        private readonly MedidorDeTempoDeJanela _medidor = MedidorDeTempoDeJanela.Instance;

        public FrmDebug()
        {
            InitializeComponent();

            _medidor.TempoMedido += Medidor_TempoMedido;
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
                lblIdProcesso.Text = janela.Processo.Id.ToString();
                lblNomeJanela.Text = janela.Titulo;
                lblExecutavel.Text = janela.Executavel;
                lblTempo.Text = janela.TempoDeAtividade.ToString(@"mm\:ss");

                DrawProcessIcon(janela);
            }));
        }

        private void DrawProcessIcon(Janela janela)
        {
            try
            {
                var ico = WindowsApi.GetProcessIcon(janela.Processo);

                picIconeProcesso.Image = ico.ToBitmap();
                picIconeProcesso.Visible = true;
            }
            catch (Exception)
            {
                picIconeProcesso.Visible = false;
            }
        }
    }
}
