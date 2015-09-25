using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SURLYcmps439.Startup))]
namespace SURLYcmps439
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
