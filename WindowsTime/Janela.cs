using System;
using System.Diagnostics;

namespace WindowsTime
{
    public class Janela
    {
        private readonly Stopwatch _medidorDetempo = new Stopwatch();

        public int WindowsHandle { get; private set; }
        public string Titulo { get; private set; }
        public Process Processo { get; private set; }
        public string Executavel { get; private set; }
        public string NomeDoExecutavel { get; set; }
        public bool EstaAtiva { get; private set; }
        public TimeSpan TempoDeAtividade { get { return _medidorDetempo.Elapsed; } }

        public Janela(int windowsHandle)
        {
            WindowsHandle = windowsHandle;
            Titulo = WindowsApi.GetWindowsText(windowsHandle);
            Processo = WindowsApi.GetProcess(windowsHandle);
            Executavel = WindowsApi.GetWindowFilePath(Processo);
            NomeDoExecutavel = WindowsApi.GetWindowFileDescription(Processo);
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

        public override string ToString()
        {
            return $"WindowsHandle: {WindowsHandle}, Titulo: {Titulo}";
        }
    }
}