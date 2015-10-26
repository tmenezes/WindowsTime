using System.Collections.Generic;
using System.Web.Http;
using WindowsTime.Core.Dados;

namespace WindowsTime.Web.Controllers
{
    public class TempoController : ApiController
    {
        public static IList<UtilizacaoDePrograma> UtilizacoesDeProgramas = new List<UtilizacaoDePrograma>();



        // GET api/tempo
        public IEnumerable<UtilizacaoDePrograma> Get()
        {
            return UtilizacoesDeProgramas;
        }

        // POST api/tempo
        public void Post([FromBody]UtilizacaoDePrograma utilizacaoDePrograma)
        {
            UtilizacoesDeProgramas.Add(utilizacaoDePrograma);
        }
    }
}
