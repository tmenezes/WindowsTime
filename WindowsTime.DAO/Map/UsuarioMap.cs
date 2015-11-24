using FluentNHibernate.Mapping;
using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO.Map
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuario");
            Id(x => x.Id);

            Map(x => x.Email).Not.Nullable().Length(255);
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.DataDeCadastro).Not.Nullable();
        }
    }
}
