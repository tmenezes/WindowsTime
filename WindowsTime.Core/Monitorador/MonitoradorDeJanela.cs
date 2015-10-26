using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Win32;
using WindowsTime.Core.Monitorador.Api;

namespace WindowsTime.Core.Monitorador
{
    public class MonitoradorDeJanela
    {
        // atributos
        private const int INTERVALO = 250;
        private static MonitoradorDeJanela _instance;
        private readonly Timer _timer;
        private Janela _ultimaJanelaAtiva;
        private bool _maquinaLockada;
        private DateTime _ultimaAtualizacaoDeProgramas;

        // propriedades
        public Dictionary<IntPtr, Janela> Janelas { get; set; }
        public static MonitoradorDeJanela Instance
        {
            get { return _instance ?? (_instance = new MonitoradorDeJanela()); }
        }
        public Janela JanelaAtiva { get { return _ultimaJanelaAtiva; } }

        // eventos
        public event EventHandler<Janela> NovaJanelaAtiva;
        public event EventHandler<Janela> TempoMedido;


        // construtor
        private MonitoradorDeJanela()
        {
            Janelas = new Dictionary<IntPtr, Janela>();

            _timer = new Timer
            {
                Interval = INTERVALO,
                Enabled = true
            };
            _timer.Elapsed += _timer_Elapsed;
        }


        // publicos
        internal void Iniciar()
        {
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

            _ultimaAtualizacaoDeProgramas = DateTime.Now;
            _timer_Elapsed(this, null);

            _timer.Start();
        }

        internal void Parar()
        {
            _timer.Stop();

            SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
        }


        // privados e etc
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_maquinaLockada)
                return;


            var handle = WindowsApi.GetActiveWindowHandle();
            var janela = GetJanelaCorrente(handle);

            if (!janela.EstaAtiva)
            {
                janela.NotificarJanelaAtiva();
                OnNovaJanelaAtiva(janela);
            }
            OnTempoMedido(janela);

            var trocouDeJanela = (janela != _ultimaJanelaAtiva) && (_ultimaJanelaAtiva != null);
            if (trocouDeJanela)
                _ultimaJanelaAtiva.NotificarJanelaInativa();

            var novoTitulo = WindowsApi.GetWindowsText(handle);
            var trocouDeTitulo = (janela == _ultimaJanelaAtiva) && janela.Titulo != novoTitulo;
            if (trocouDeTitulo)
                janela.NotificarMudancaDeTitulo(novoTitulo);

            _ultimaJanelaAtiva = janela;

            AtualizarProgramasDasJanelas();
        }


        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _maquinaLockada = true;

                if (_ultimaJanelaAtiva != null)
                    _ultimaJanelaAtiva.NotificarJanelaInativa();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                _maquinaLockada = false;
            }
        }

        private Janela GetJanelaCorrente(IntPtr handle)
        {
            var janelaJaMonitorada = Janelas.ContainsKey(handle);

            var janela = (janelaJaMonitorada)
                             ? Janelas[handle]
                             : new Janela(handle);

            if (!janelaJaMonitorada)
                Janelas.Add(handle, janela);

            return janela;
        }

        private void AtualizarProgramasDasJanelas()
        {
            var deveAtualizarProgramas = (DateTime.Now - _ultimaAtualizacaoDeProgramas).TotalSeconds > 5;
            if (deveAtualizarProgramas)
            {
                var janelasWindowsStore = Instance.Janelas.Values.Where(j => j.Programa.Tipo == TipoDePrograma.WindowsStore &&
                                                                             WindowsStoreApi.IsFrameHostProcess(j.Programa.Processo));
                foreach (var janela in janelasWindowsStore)
                {
                    janela.AtualizarPrograma();
                }

                _ultimaAtualizacaoDeProgramas = DateTime.Now;
            }
        }

        protected virtual void OnNovaJanelaAtiva(Janela janela)
        {
            var handler = NovaJanelaAtiva;
            if (handler != null) handler(this, janela);
        }
        protected virtual void OnTempoMedido(Janela e)
        {
            var handler = TempoMedido;
            if (handler != null) handler(this, e);
        }
    }
}