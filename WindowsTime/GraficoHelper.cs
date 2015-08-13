using System.Collections.Generic;
using System.Linq;

namespace WindowsTime
{
    public static class GraficoHelper
    {
        internal static IEnumerable<JanelaPorExecutavel> GetJanelasAgrupadasPorExecutavel()
        {
            var janelas = MedidorDeTempoDeJanela.Instance.Janelas.Values
                                                .GroupBy(j => j.NomeDoExecutavel)
                                                .Select(group => new JanelaPorExecutavel()
                                                {
                                                    Executavel = group.Key,
                                                    Processo = group.First().Processo,
                                                    Tempo = group.Sum(i => i.TempoDeAtividade.TotalSeconds),
                                                    TotalJanelas = group.Sum(i => i.ToralDeAbasVisitadas),
                                                    Icone = group.First().Icone,
                                                })
                                                .OrderByDescending(i => i.Tempo)
                                                .ToList();
            return janelas;
        }

        internal static IEnumerable<JanelaPorExecutavel> GetJanelasAgrupadasPorExecutavel(string executavelAlvo)
        {
            var janelas = MedidorDeTempoDeJanela.Instance.Janelas.Values
                                                .GroupBy(j => (j.NomeDoExecutavel == executavelAlvo) ? executavelAlvo : "Outros")
                                                .Select(group => new JanelaPorExecutavel()
                                                {
                                                    Executavel = group.Key,
                                                    Processo = group.First().Processo,
                                                    Tempo = group.Sum(i => i.TempoDeAtividade.TotalSeconds),
                                                    TotalJanelas = group.Sum(i => i.ToralDeAbasVisitadas),
                                                    Icone = group.First().Icone,
                                                })
                                                .OrderByDescending(i => i.Tempo)
                                                .ToList();
            return janelas;
        }
    }
}
