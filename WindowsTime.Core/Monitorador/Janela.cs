using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowsTime.Core.Monitorador.Api;

namespace WindowsTime.Core.Monitorador
{
    public class Janela
    {
        private TimeSpan _tempoMedidoSincronizado = TimeSpan.Zero;
        private readonly Stopwatch _medidorDeTempo = new Stopwatch();

        public IntPtr WindowsHandle { get; private set; }
        public string Titulo { get; private set; }
        public Programa Programa { get; private set; }
        public bool EstaAtiva { get; private set; }

        public TimeSpan TempoDeAtividadeTotal { get { return _medidorDeTempo.Elapsed; } }
        public TimeSpan TempoNaoSincronizado { get { return _medidorDeTempo.Elapsed - _tempoMedidoSincronizado; } }


        public Janela(IntPtr windowsHandle)
        {
            WindowsHandle = windowsHandle;
            Titulo = WindowsApi.GetWindowsText(windowsHandle);
            Programa = Programa.Desconhecido(this);

            // carrega o programa corretamente
            CarregarPrograma();
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

        public TimeSpan CalcularTempoDeAtividadeNaoSincronizado()
        {
            var tempo = _medidorDeTempo.Elapsed - _tempoMedidoSincronizado;
            _tempoMedidoSincronizado = tempo;

            return tempo;
        }

        public void CarregarPrograma()
        {
            Task.Factory.StartNew(() => Programa = Programa.Carregar(this));
        }

        public override string ToString()
        {
            return $"WindowsHandle: {WindowsHandle}, Titulo: {Titulo}";
        }
    }
}