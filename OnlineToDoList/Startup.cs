using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineToDoList.Startup))]
namespace OnlineToDoList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
