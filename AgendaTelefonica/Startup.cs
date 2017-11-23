using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgendaTelefonica.Startup))]
namespace AgendaTelefonica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
