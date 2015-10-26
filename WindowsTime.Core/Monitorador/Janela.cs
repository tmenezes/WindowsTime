using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowsTime.Core.Monitorador.Api;

namespace WindowsTime.Core.Monitorador
{
    public class Janela
    {
        private readonly Stopwatch _medidorDeTempo = new Stopwatch();

        public IntPtr WindowsHandle { get; private set; }
        public string Titulo { get; private set; }
        public Programa Programa { get; private set; }
        public bool EstaAtiva { get; private set; }

        public TimeSpan TempoDeAtividade { get { return _medidorDeTempo.Elapsed; } }


        public Janela(IntPtr windowsHandle)
        {
            WindowsHandle = windowsHandle;
            Titulo = WindowsApi.GetWindowsText(windowsHandle);
            Programa = Programa.Desconhecido(this);

            // carrega o programa corretamente
            AtualizarPrograma();
        }


        public void NotificarJanelaAtiva()
        {
            EstaAtiva = true;

            _medidorDeTempo.Start();
        }

        public void NotificarJanelaInativa()
        {
            EstaAtiva = false;

            _medidorDeTempo.Stop();
        }

        public void NotificarMudancaDeTitulo(string novoTitulo)
        {
            Titulo = novoTitulo;

            Programa.NotificarNovaAreaAcessada(novoTitulo);
        }

        public void AtualizarPrograma()
        {
            Task.Factory.StartNew(() => Programa = Programa.Carregar(this));
        }

        public override string ToString()
        {
            return $"WindowsHandle: {WindowsHandle}, Titulo: {Titulo}";
        }
    }
}