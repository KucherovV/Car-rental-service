using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Car_Rental_Service.Startup))]
namespace Car_Rental_Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
