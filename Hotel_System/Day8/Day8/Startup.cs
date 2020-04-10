using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Day8.Startup))]
namespace Day8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
