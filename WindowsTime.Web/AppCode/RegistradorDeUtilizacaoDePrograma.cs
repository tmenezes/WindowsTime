using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsTime.Core.DTO;
using WindowsTime.Dominio.Repository;

namespace WindowsTime.Web.AppCode
{
    public class RegistradorDeUtilizacaoDePrograma
    {
        private static readonly object _syncObject = new object();
        private static volatile RegistradorDeUtilizacaoDePrograma _instance;
        private readonly ITempoRepository _tempoRepository;
        private readonly IProgramaRepository _programaRepository;
        private readonly ConcurrentQueue<UtilizacaoDTO> _utilizacoesDeProgramas = new ConcurrentQueue<UtilizacaoDTO>();
        private readonly Task _taskRegistrarUtilizacao;

        // construtores
        private RegistradorDeUtilizacaoDePrograma(ITempoRepository tempoRepository, IProgramaRepository programaRepository)
        {
            _tempoRepository = tempoRepository;
            _programaRepository = programaRepository;

            _taskRegistrarUtilizacao = new Task(ProcessarRegistroDeUtilizacoesInfinitamente, TaskCreationOptions.LongRunning);
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
                    _instance = new RegistradorDeUtilizacaoDePrograma(new TempoRepository(), new ProgramaRepository());
                    _instance._taskRegistrarUtilizacao.Start();
                }
            }
        }

        public void SolicitarRegistroDeUtilizacao(UtilizacaoDTO utilizacaoDTO)
        {
            _utilizacoesDeProgramas.Enqueue(utilizacaoDTO);
        }


        // privados
        private void ProcessarRegistroDeUtilizacoesInfinitamente()
        {
            while (true) // loop infinito
            {
                while (_utilizacoesDeProgramas.Any())
                {
                    UtilizacaoDTO utilizacaoDTO;

                    if (!_utilizacoesDeProgramas.TryDequeue(out utilizacaoDTO))
                        continue;

                    RegistrarUtilizacaoDeProgramas(utilizacaoDTO);
                }

                Thread.Sleep(500);
            }
        }

        private void RegistrarUtilizacaoDeProgramas(UtilizacaoDTO utilizacaoDTO)
        {
            var utilizacaoDoDia = _tempoRepository.ObterUtilizacaoDeProgramasDoDia(null);
            if (utilizacaoDoDia == null)
            {
                //_tempoRepository.Salvar(utilizacaoDTO);
            }
            else
            {
                //utilizacaoDoDia.Programas.Union(uti)
            }
        }

        private void AtribuirProgramaCorreto(UtilizacaoDTO utilizacaoDTO)
        {
            
        }
    }
}
