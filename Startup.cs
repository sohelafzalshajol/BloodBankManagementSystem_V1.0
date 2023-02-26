using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodBankMVC.Startup))]
namespace BloodBankMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
