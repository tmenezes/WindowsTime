using System;
using System.Linq;
using NHibernate.Linq;
using WindowsTime.Core.Dominio;
using WindowsTime.Infraestrutura.DAO.Repository;

namespace WindowsTime.DAO
{
    public class AtividadeDoUsuarioRepository : RepositoryBase<AtividadeDoUsuario>, IAtividadeDoUsuarioRepository
    {
        public AtividadeDoUsuario ObterAtividadeDoUsuarioDoDia(Usuario usuario)
        {
            var dataInicio = DateTime.Now.Date;
            var dataFim = dataInicio.AddDays(1).AddSeconds(-1);

            return RepositoryMediator.LinqQuery().Fetch(p => p.Usuario)
                                                 .FirstOrDefault(p => p.Usuario.Id == usuario.Id
                                                                   && p.Data >= dataInicio
                                                                   && p.Data <= dataFim);

        }
    }
}
