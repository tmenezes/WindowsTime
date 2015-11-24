using FluentNHibernate.Mapping;
using WindowsTime.Core.Dominio;

namespace WindowsTime.Infraestrutura.DAO.Map
{
    public class AtividadeMap : ClassMap<AtividadeDoUsuario>
    {
        public AtividadeMap()
        {
            Table("Atividade");
            Id(x => x.Id);

            References(x => x.Usuario).Columns("IdUsuario").Cascade.None();

            Map(x => x.Data).Not.Nullable();

            HasMany(x => x.Janelas)
                .KeyColumns.Add("IdAtividade")
                .Inverse()
                .Cascade.All()
                .LazyLoad();
        }
    }
}