using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using WindowsTime.Properties;

namespace WindowsTime
{
    public class Janela
    {
        private readonly Stopwatch _medidorDetempo = new Stopwatch();
        private readonly Dictionary<string, bool> _abasVisitadas = new Dictionary<string, bool>();
        private Image _icone;

        public int WindowsHandle { get; private set; }
        public string Titulo { get; private set; }
        public Process Processo { get; private set; }
        public string Executavel { get; private set; }
        public string NomeDoExecutavel { get; set; }
        public bool EstaAtiva { get; private set; }
        public int ToralDeAbasVisitadas { get; private set; }

        public TimeSpan TempoDeAtividade { get { return _medidorDetempo.Elapsed; } }
        public IEnumerable<string> AbasVisitadas { get { return _abasVisitadas.Keys.ToList(); } }
        public Image Icone { get { return _icone ?? (_icone = (WindowsApi.GetProcessIcon(Processo) ?? Resources.windows).ToBitmap()); } } // TODO: Melhorar


        public Janela(int windowsHandle)
        {
            WindowsHandle = windowsHandle;
            Titulo = WindowsApi.GetWindowsText(windowsHandle);
            Processo = WindowsApi.GetProcess(windowsHandle);
            Executavel = WindowsApi.GetWindowFilePath(Processo);
            NomeDoExecutavel = WindowsApi.GetWindowFileDescription(Processo);
            ToralDeAbasVisitadas = 1;

            _abasVisitadas.Add(Titulo, true);
        }


        public void NotificarJanelaAtiva()
        {
            EstaAtiva = true;

            _medidorDetempo.Start();
        }

        public void NotificarJanelaInativa()
        {
            EstaAtiva = false;

            _medidorDetempo.Stop();
        }

        public void NotificarNovaAreaOuAbaVisitada(string novoTitulo)
        {
            Titulo = novoTitulo;

            if (!_abasVisitadas.ContainsKey(novoTitulo))
            {
                _abasVisitadas.Add(novoTitulo, true);
                ToralDeAbasVisitadas++;
            }
        }

        public override string ToString()
        {
            return string.Format("WindowsHandle: {0}, Titulo: {1}", WindowsHandle, Titulo);
        }
    }
}