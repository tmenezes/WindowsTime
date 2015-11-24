using WindowsTime.Core.Dominio;
using FluentNHibernate.Mapping;

namespace WindowsTime.DAO.Map
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