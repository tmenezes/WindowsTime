using FluentNHibernate.Mapping;
using WindowsTime.Core.Dominio;

namespace WindowsTime.Infraestrutura.DAO.Map
{
    public class JanelaMap : ClassMap<Janela>
    {
        public JanelaMap()
        {
            Table("Janela");
            Id(x => x.Id);

            References(x => x.Programa).Columns("IdPrograma").Cascade.SaveUpdate();

            Map(x => x.Titulo).Not.Nullable().Length(255);
        }
    }
}