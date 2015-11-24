using System.Collections.Generic;
using System.Web.Http;
using WindowsTime.Core.DTO;
using WindowsTime.Web.AppCode;

namespace WindowsTime.Web.Controllers
{
    public class AtividadeController : ApiController
    {
        private readonly RegistradorDeAtividadeDoUsuario _registradorDeAtividadeDoUsuario;

        // construtor
        public AtividadeController() : this(RegistradorDeAtividadeDoUsuario.Instancia)
        {
        }

        public AtividadeController(RegistradorDeAtividadeDoUsuario registradorDeAtividadeDoUsuario)
        {
            _registradorDeAtividadeDoUsuario = registradorDeAtividadeDoUsuario;
        }


        // GET api/tempo
        public IEnumerable<AtividadeDoUsuarioDTO> Get()
        {
            return null;
        }

        // POST api/tempo
        public void Post([FromBody]AtividadeDoUsuarioDTO atividadeDoUsuarioDTO)
        {
            _registradorDeAtividadeDoUsuario.SolicitarRegistroDeAtividade(atividadeDoUsuarioDTO);
        }
    }
}
