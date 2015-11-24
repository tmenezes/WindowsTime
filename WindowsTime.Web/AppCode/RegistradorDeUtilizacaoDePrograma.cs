using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsTime.Core.DTO;
using WindowsTime.Dominio;
using WindowsTime.Dominio.Repository;

namespace WindowsTime.Web.AppCode
{
    public class RegistradorDeUtilizacaoDePrograma
    {
        private static readonly object _syncObject = new object();
        private static volatile RegistradorDeUtilizacaoDePrograma _instance;
        private readonly IAtividadeDoUsuarioRepository _atividadeDoUsuarioRepository;
        private readonly IProgramaRepository _programaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ConcurrentQueue<UtilizacaoDTO> _utilizacoesDeProgramas = new ConcurrentQueue<UtilizacaoDTO>();
        private readonly Task _taskRegistrarUtilizacao;

        // construtores
        private RegistradorDeUtilizacaoDePrograma(IAtividadeDoUsuarioRepository atividadeDoUsuarioRepository, IProgramaRepository programaRepository,
                                                  IUsuarioRepository usuarioRepository)
        {
            _atividadeDoUsuarioRepository = atividadeDoUsuarioRepository;
            _programaRepository = programaRepository;
            _usuarioRepository = usuarioRepository;

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
                    _instance = new RegistradorDeUtilizacaoDePrograma(new AtividadeDoUsuarioRepository(), new ProgramaRepository(), new UsuarioRepository());
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
            var usuario = _usuarioRepository.ObterUsuario(utilizacaoDTO.EmailDoUsuario);
            var atividadeDoDia = _atividadeDoUsuarioRepository.ObterAtividadeDoUsuarioDoDia(usuario) ?? new AtividadeDoUsuario(usuario);

            var janelas = utilizacaoDTO.Programas.SelectMany(p => p.Janelas, (prog, jan) =>
            {
                var programa = _programaRepository.ObterPrograma(prog.Nome) ?? new Programa(prog.Nome);
                var janela = new Janela(jan.Titulo, programa, jan.TempoDeUtilizacaoTotal);

                return janela;
            });

            atividadeDoDia.Janelas = janelas;

            _atividadeDoUsuarioRepository.Salvar(atividadeDoDia);
        }
    }
}
