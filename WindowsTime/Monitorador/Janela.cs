using System;
using System.Diagnostics;
using WindowsTime.Monitorador.Api;

namespace WindowsTime.Monitorador
{
    public class Janela
    {
        private readonly Stopwatch _medidorDetempo = new Stopwatch();

        public int WindowsHandle { get; private set; }
        public string Titulo { get; private set; }
        public Programa Programa { get; private set; }
        public bool EstaAtiva { get; private set; }

        public TimeSpan TempoDeAtividade { get { return _medidorDetempo.Elapsed; } }


        public Janela(int windowsHandle)
        {
            WindowsHandle = windowsHandle;
            Titulo = WindowsApi.GetWindowsText(windowsHandle);
            Programa = Programa.Criar(windowsHandle, Titulo);
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

        public void NotificarMudancaDeTitulo(string novoTitulo)
        {
            Titulo = novoTitulo;

            Programa.NotificarNovaAreaAcessada(novoTitulo);
        }

        public override string ToString()
        {
            return string.Format("WindowsHandle: {0}, Titulo: {1}", WindowsHandle, Titulo);
        }
    }
}