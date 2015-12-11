using System;
using System.Collections.Generic;
using System.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.Infraestrutura.DAO.Repository;

namespace WindowsTime.DAO
{
    public class RelatoriosGeraisDAO
    {
        protected RepositoryMediator<Janela> Mediator { get; set; }

        public RelatoriosGeraisDAO()
        {
            Mediator = new RepositoryMediator<Janela>();
        }


        public IEnumerable<Programa> ObterProgramasMaisUtilizadosDaSemana()
        {
            var dataInicio = DateTime.Now.AddDays(-7);
            var dataFim = DateTime.Now;

            var programas = Mediator.LinqQuery().Where(j => j.Atividade.Data >= dataInicio
                                                         && j.Atividade.Data <= dataFim)
                                                .GroupBy(j => j.Programa.Nome)
                                                .Select(group => new Programa()
                                                {
                                                    Nome = group.Key,
                                                    TempoDeAtividade = group.Sum(j => j.TempoDeAtividade)
                                                }).ToList();

            return programas;
        }
    }
}
