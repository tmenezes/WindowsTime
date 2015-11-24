using FluentNHibernate.Mapping;
using WindowsTime.Core.Dominio;

namespace WindowsTime.Infraestrutura.DAO.Map
{
    public class ProgramaMap : ClassMap<Programa>
    {
        public ProgramaMap()
        {
            Table("Programa");
            Id(x => x.Id);

            Map(x => x.Nome).Not.Nullable().Length(100);
        }
    }
}