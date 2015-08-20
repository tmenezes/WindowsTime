using System.Collections.Generic;
using System.Linq;

namespace WindowsTime
{
    public static class GraficoHelper
    {
        internal static IEnumerable<DadosDoPrograma> GetProgramas()
        {
            var janelas = MonitoradorDeJanela.Instance.Janelas.Values
                                             .GroupBy(j => j.Programa.Nome)
                                             .Select(group => new DadosDoPrograma()
                                             {
                                                 Nome = group.Key,
                                                 TempoDeUtilizacao = group.Sum(i => i.TempoDeAtividade.TotalSeconds),
                                                 TotalJanelas = group.Sum(i => i.Programa.TotalDeAreasVisitadas),
                                                 Icone = group.First().Programa.Icone,
                                             })
                                             .OrderByDescending(i => i.TempoDeUtilizacao)
                                             .ToList();
            return janelas;
        }

        internal static IEnumerable<DadosDoPrograma> GetProgramas(string programaAlvo)
        {
            var janelas = MonitoradorDeJanela.Instance.Janelas.Values
                                             .GroupBy(j => (j.Programa.Nome == programaAlvo) ? programaAlvo : "Outros")
                                             .Select(group => new DadosDoPrograma()
                                             {
                                                 Nome = group.Key,
                                                 TempoDeUtilizacao = group.Sum(i => i.TempoDeAtividade.TotalSeconds),
                                                 TotalJanelas = group.Sum(i => i.Programa.TotalDeAreasVisitadas),
                                                 Icone = group.First().Programa.Icone,
                                             })
                                             .OrderByDescending(i => i.TempoDeUtilizacao)
                                             .ToList();
            return janelas;
        }
    }
}
