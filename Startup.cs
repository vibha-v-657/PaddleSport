using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hackathon_Internship.Startup))]
namespace Hackathon_Internship
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
