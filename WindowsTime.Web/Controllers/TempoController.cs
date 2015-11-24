using System.Collections.Generic;
using System.Web.Http;
using WindowsTime.Core.DTO;
using WindowsTime.Dominio.Repository;

namespace WindowsTime.Web.Controllers
{
    public class TempoController : ApiController
    {
        private readonly IAtividadeDoUsuarioRepository _atividadeDoUsuarioRepository;

        // construtor
        public TempoController()
        {

        }

        public TempoController(IAtividadeDoUsuarioRepository atividadeDoUsuarioRepository)
        {
            _atividadeDoUsuarioRepository = atividadeDoUsuarioRepository;
        }


        // GET api/tempo
        public IEnumerable<UtilizacaoDTO> Get()
        {
            return null;
        }

        // POST api/tempo
        public void Post([FromBody]UtilizacaoDTO utilizacaoDTO)
        {
        }
    }
}
