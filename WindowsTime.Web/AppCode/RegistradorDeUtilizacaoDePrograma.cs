using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsTime.Core.Dados;
using WindowsTime.DAO;

namespace WindowsTime.Web.AppCode
{
    public class RegistradorDeUtilizacaoDePrograma
    {
        private static readonly object _syncObject = new object();
        private static volatile RegistradorDeUtilizacaoDePrograma _instance;
        private readonly ITempoRepository _tempoRepository;
        private readonly ConcurrentQueue<UtilizacaoDePrograma> _utilizacoesDeProgramas = new ConcurrentQueue<UtilizacaoDePrograma>();
        private readonly Task _taskRegistrarUtilizacao;

        // construtores
        private RegistradorDeUtilizacaoDePrograma(ITempoRepository tempoRepository)
        {
            _tempoRepository = tempoRepository;

            _taskRegistrarUtilizacao = new Task(RegistrarUtilizacoesInfinitamente, TaskCreationOptions.LongRunning);
        }


        // publicos
        public static void Iniciar()
        {
            if (_instance != null)
                return;

            lock (_syncObject)
            {
                if (_instance == null)
                {
                    _instance = new RegistradorDeUtilizacaoDePrograma(new TempoRepository());
                    _instance._taskRegistrarUtilizacao.Start();
                }
            }
        }

        public void RegistrarUtilizacao(UtilizacaoDePrograma utilizacaoDePrograma)
        {
            _utilizacoesDeProgramas.Enqueue(utilizacaoDePrograma);
        }


        // privados
        private void RegistrarUtilizacoesInfinitamente()
        {
            while (true) // loop infinito
            {
                while (_utilizacoesDeProgramas.Any())
                {
                    UtilizacaoDePrograma utilizacaoDePrograma;

                    if (!_utilizacoesDeProgramas.TryDequeue(out utilizacaoDePrograma))
                        continue;

                    //var utilizacaoDoDia = _tempoRepository.ObterUtilizacaoDeProgramasDoDia(utilizacaoDePrograma.Usuario);
                    //if (utilizacaoDoDia == null)
                    //{
                    //    _tempoRepository.Salvar(utilizacaoDePrograma);
                    //}
                    //else
                    //{
                    //    utilizacaoDoDia.Programas.Union(uti)
                    //}
                }

                Thread.Sleep(500);
            }
        }
    }
}
