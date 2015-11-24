using Owin;
using WindowsTime.Web.AppCode;

namespace WindowsTime.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegistradorDeAtividadeDoUsuario.Iniciar();
        }
    }
}
