using Owin;
using WindowsTime.DAO;
using WindowsTime.Infraestrutura.DAO;
using WindowsTime.Web.AppCode;

namespace WindowsTime.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DataBase.Inicializar(new SessionFactoryHolder());
            RegistradorDeAtividadeDoUsuario.Iniciar();
        }
    }
}
