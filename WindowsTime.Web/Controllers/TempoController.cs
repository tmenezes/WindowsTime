using System.Collections.Generic;
using System.Web.Http;
using WindowsTime.Core.Dados;
using WindowsTime.DAO;

namespace WindowsTime.Web.Controllers
{
    public class TempoController : ApiController
    {
        private readonly ITempoRepository _tempoRepository;

        // construtor
        public TempoController()
        {

        }

        public TempoController(ITempoRepository tempoRepository)
        {
            _tempoRepository = tempoRepository;
        }


        // GET api/tempo
        public IEnumerable<UtilizacaoDePrograma> Get()
        {
            return null;
        }

        // POST api/tempo
        public void Post([FromBody]UtilizacaoDePrograma utilizacaoDePrograma)
        {
        }
    }
}
