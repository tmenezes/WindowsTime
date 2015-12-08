using Owin;
using WindowsTime.DAO.Map;
using WindowsTime.Infraestrutura.DAO;
using WindowsTime.Web.AppCode;

namespace WindowsTime.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DataBase.Inicializar<UsuarioMap>(new SessionFactoryHolder());
            RegistradorDeAtividadeDoUsuario.Iniciar();
        }
    }
}
