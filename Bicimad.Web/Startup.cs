using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bicimad.Web.Startup))]
namespace Bicimad.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
