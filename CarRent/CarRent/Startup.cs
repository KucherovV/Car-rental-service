using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRent.Startup))]
namespace CarRent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
