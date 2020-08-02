using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PantryManager.Startup))]
namespace PantryManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
