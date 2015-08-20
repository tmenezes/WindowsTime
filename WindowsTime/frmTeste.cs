using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsTime
{
    public partial class frmTeste : Form
    {
        public frmTeste(string executavel)
        {
            InitializeComponent();

            CarregarIcones(executavel);
        }

        private void CarregarIcones(string executavel)
        {
            var icones = new List<IconeWindows>
                         {
                             new IconeWindows(SystemIcons.Application.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Asterisk.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Error.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Exclamation.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Hand.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Information.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Question.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Shield.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.Warning.ToBitmap(), 0, "SystemIcons"),
                             new IconeWindows(SystemIcons.WinLogo.ToBitmap(), 0, "SystemIcons"),
                         };

            string filename = executavel.Substring(executavel.LastIndexOf('\\') + 1);
            for (int i = 0; i < 10; i++)
            {
                var icone = WindowsApi.GetIcon(executavel, i, true);
                if (icone != null)
                {
                    icones.Add(new IconeWindows(icone.ToBitmap(), i, filename));
                }
            }

            for (int i = 0; i < 512; i++)
            {
                var icone = WindowsApi.GetWindowsShell32Icon(i, true);
                if (icone != null)
                {
                    icones.Add(new IconeWindows(icone.ToBitmap(), i, "Shell32"));
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var icone = WindowsApi.GetExplorerIcon(i, true);
                if (icone != null)
                {
                    icones.Add(new IconeWindows(icone.ToBitmap(), i, "Explorer"));
                }
            }



            var source = new BindingSource { DataSource = icones };

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;
        }
    }

    internal class IconeWindows
    {
        public Image Icone { get; set; }
        public int Index { get; set; }
        public string Source { get; set; }

        public IconeWindows(Image icone, int index, string source)
        {
            Icone = icone;
            Index = index;
            Source = source;
        }
    }
}
