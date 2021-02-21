using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Metasoft.Startup))]
namespace Metasoft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
