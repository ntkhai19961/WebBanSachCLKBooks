using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanSachCLKBooks.Startup))]
namespace WebBanSachCLKBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
