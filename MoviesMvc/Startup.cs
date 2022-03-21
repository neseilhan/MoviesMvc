using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviesMvc.Startup))]
namespace MoviesMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
