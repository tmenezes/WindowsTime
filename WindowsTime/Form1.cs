using System;
using System.Windows.Forms;

namespace WindowsTime
{
    public partial class Form1 : Form
    {
        private readonly MedidorDeTempoDeJanela _medidor = MedidorDeTempoDeJanela.Instance;

        public Form1()
        {
            InitializeComponent();

            _medidor.NovaJanelaAtiva += Medidor_NovaJanelaAtiva;
            _medidor.Iniciar();

            timer1.Enabled = true;
            timer1.Start();
        }


        private void Medidor_NovaJanelaAtiva(object sender, Janela janela)
        {
            this.Invoke((Action)(() =>
            {
                lblHandle.Text = janela.WindowsHandle.ToString();
                lblIdProcesso.Text = janela.Processo.Id.ToString();
                lblNomeJanela.Text = janela.Titulo;
                lblExecutavel.Text = janela.NomeDoExecutavel;

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_medidor.JanelaAtiva != null)
            {
                lblTempo.Text = _medidor.JanelaAtiva.TempoDeAtividade.ToString(@"mm\:ss");
            }
        }
    }
}
