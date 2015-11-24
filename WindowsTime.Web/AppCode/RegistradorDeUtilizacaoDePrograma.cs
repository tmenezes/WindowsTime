using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsTime.Core.DTO;
using WindowsTime.Dominio;
using WindowsTime.Dominio.Repository;

namespace WindowsTime.Web.AppCode
{
    public class RegistradorDeAtividadeDoUsuario
    {
        private static readonly object _syncObject = new object();
        private static volatile RegistradorDeAtividadeDoUsuario _instance;

        private readonly IAtividadeDoUsuarioRepository _atividadeDoUsuarioRepository;
        private readonly IProgramaRepository _programaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ConcurrentQueue<AtividadeDoUsuarioDTO> _utilizacoesDeProgramas = new ConcurrentQueue<AtividadeDoUsuarioDTO>();
        private readonly Task _taskRegistrarUtilizacao;

        public static RegistradorDeAtividadeDoUsuario Instancia => _instance;

        // construtores
        public RegistradorDeAtividadeDoUsuario(IAtividadeDoUsuarioRepository atividadeDoUsuarioRepository, IProgramaRepository programaRepository,
                                                IUsuarioRepository usuarioRepository)
        {
            _atividadeDoUsuarioRepository = atividadeDoUsuarioRepository;
            _programaRepository = programaRepository;
            _usuarioRepository = usuarioRepository;

            _taskRegistrarUtilizacao = new Task(ProcessarRegistrosDeAtividadesInfinitamente, TaskCreationOptions.LongRunning);
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
                    _instance = new RegistradorDeAtividadeDoUsuario(new AtividadeDoUsuarioRepository(), new ProgramaRepository(), new UsuarioRepository());
                    _instance._taskRegistrarUtilizacao.Start();
                }
            }
        }

        public void SolicitarRegistroDeAtividade(AtividadeDoUsuarioDTO atividadeDoUsuarioDTO)
        {
            _utilizacoesDeProgramas.Enqueue(atividadeDoUsuarioDTO);
        }


        // privados
        private void ProcessarRegistrosDeAtividadesInfinitamente()
        {
            while (true) // loop infinito
            {
                while (_utilizacoesDeProgramas.Any())
                {
                    AtividadeDoUsuarioDTO atividadeDoUsuarioDTO;

                    if (!_utilizacoesDeProgramas.TryDequeue(out atividadeDoUsuarioDTO))
                        continue;

                    RegistrarUtilizacaoDeProgramas(atividadeDoUsuarioDTO);
                }

                Thread.Sleep(500);
            }
        }

        private void RegistrarUtilizacaoDeProgramas(AtividadeDoUsuarioDTO atividadeDoUsuarioDTO)
        {
            var usuario = _usuarioRepository.ObterUsuario(atividadeDoUsuarioDTO.EmailDoUsuario);
            var atividadeDoDia = _atividadeDoUsuarioRepository.ObterAtividadeDoUsuarioDoDia(usuario) ?? new AtividadeDoUsuario(usuario);

            atividadeDoUsuarioDTO.Programas.SelectMany(p => p.Janelas, (prog, jan) =>
            {
                var programa = _programaRepository.ObterPrograma(prog.Nome) ?? new Programa(prog.Nome);
                var janela = new Janela(jan.Titulo, programa, jan.TempoDeUtilizacaoTotal);

                atividadeDoDia.Janelas.Add(janela);
                return janela;
            })
            .ToList();

            _atividadeDoUsuarioRepository.Salvar(atividadeDoDia);
        }
    }
}
