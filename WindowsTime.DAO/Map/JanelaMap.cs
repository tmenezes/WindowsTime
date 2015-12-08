using FluentNHibernate.Mapping;
using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO.Map
{
    public class JanelaMap : ClassMap<Janela>
    {
        public JanelaMap()
        {
            Table("Janela");
            Id(x => x.Id);

            References(x => x.Atividade).Columns("IdAtividade").Cascade.None();
            References(x => x.Programa).Columns("IdPrograma").Cascade.SaveUpdate();

            Map(x => x.Titulo).Not.Nullable().Length(255);
            Map(x => x.TempoDeAtividade).Not.Nullable().Length(12).Scale(6);
        }
    }
}