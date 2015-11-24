using System.Collections.Generic;
using System.Linq;
using WindowsTime.Core.DTO;
using WindowsTime.Core.Monitorador;

namespace WindowsTime
{
    public static class GraficoHelper
    {
        internal static IEnumerable<ProgramaDTO> GetProgramas()
        {
            var janelas = MonitoradorDeJanela.Instance.Janelas.Values
                                             .GroupBy(j => j.Programa.Nome)
                                             .Select(group => new ProgramaDTO()
                                             {
                                                 Nome = group.Key,
                                                 TempoDeUtilizacao = group.Sum(i => i.TempoDeAtividadeTotal.TotalSeconds),
                                                 TotalJanelas = group.Sum(i => i.Programa.TotalDeAreasVisitadas),
                                                 Icone = group.First().Programa.Icone,
                                             })
                                             .OrderByDescending(i => i.TempoDeUtilizacao)
                                             .ToList();
            return janelas;
        }

        internal static IEnumerable<ProgramaDTO> GetProgramas(string programaAlvo)
        {
            var janelas = MonitoradorDeJanela.Instance.Janelas.Values
                                             .GroupBy(j => (j.Programa.Nome == programaAlvo) ? programaAlvo : "Outros")
                                             .Select(group => new ProgramaDTO()
                                             {
                                                 Nome = group.Key,
                                                 TempoDeUtilizacao = group.Sum(i => i.TempoDeAtividadeTotal.TotalSeconds),
                                                 TotalJanelas = group.Sum(i => i.Programa.TotalDeAreasVisitadas),
                                                 Icone = group.First().Programa.Icone,
                                             })
                                             .OrderByDescending(i => i.TempoDeUtilizacao)
                                             .ToList();
            return janelas;
        }
    }
}
