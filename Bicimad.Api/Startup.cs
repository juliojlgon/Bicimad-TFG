using Bicimad.Api;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Bicimad.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
