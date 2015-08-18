using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using WindowsTime.Properties;

namespace WindowsTime
{
    public class Programa
    {
        private readonly Dictionary<string, bool> _areasVisitadas = new Dictionary<string, bool>();
        private Image _icone;

        public Process Processo { get; private set; }
        public string Nome { get; set; }
        public string Executavel { get; private set; }
        public int TotalDeAreasVisitadas { get; private set; }

        public IEnumerable<string> AreasVisitadas { get { return _areasVisitadas.Keys.ToList(); } }
        public Image Icone { get { return _icone ?? (_icone = (WindowsApi.GetProcessIcon(Processo) ?? Resources.windows).ToBitmap()); } } // TODO: Melhorar


        // construtor
        public Programa(int windowsHandle, string titulo)
        {
            Processo = WindowsApi.GetProcess(windowsHandle);
            Nome = WindowsApi.GetWindowFileDescription(Processo);
            Executavel = WindowsApi.GetWindowFilePath(Processo);
            TotalDeAreasVisitadas = 1;

            _areasVisitadas.Add(titulo, true);
        }


        public void NotificarNovaAreaAcessada(string novaArea)
        {
            if (!_areasVisitadas.ContainsKey(novaArea))
            {
                _areasVisitadas.Add(novaArea, true);
                TotalDeAreasVisitadas++;
            }
        }
    }
}