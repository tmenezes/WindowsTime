using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsTime
{
    public class Janela
    {
        private readonly Stopwatch _medidorDetempo = new Stopwatch();
        private readonly Dictionary<string, bool> _titulosConhecidos = new Dictionary<string, bool>();

        public int WindowsHandle { get; private set; }
        public string Titulo { get; private set; }
        public Process Processo { get; private set; }
        public string Executavel { get; private set; }
        public string NomeDoExecutavel { get; set; }
        public bool EstaAtiva { get; private set; }
        public TimeSpan TempoDeAtividade { get { return _medidorDetempo.Elapsed; } }
        public int AreaOuAbasVisitadas { get; private set; }

        public Janela(int windowsHandle)
        {
            WindowsHandle = windowsHandle;
            Titulo = WindowsApi.GetWindowsText(windowsHandle);
            Processo = WindowsApi.GetProcess(windowsHandle);
            Executavel = WindowsApi.GetWindowFilePath(Processo);
            NomeDoExecutavel = WindowsApi.GetWindowFileDescription(Processo);
            AreaOuAbasVisitadas = 1;

            _titulosConhecidos.Add(Titulo, true);
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

            if (!_titulosConhecidos.ContainsKey(novoTitulo))
            {
                _titulosConhecidos.Add(novoTitulo, true);
                AreaOuAbasVisitadas++;
            }
        }

        public override string ToString()
        {
            return string.Format("WindowsHandle: {0}, Titulo: {1}", WindowsHandle, Titulo);
        }
    }
}