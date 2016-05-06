using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDo.MVC5.Startup))]
namespace ToDo.MVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
