using Autofac;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieSheduler.Presentation.Startup))]
namespace MovieSheduler.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IContainer container = ConfigureAutofac();
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
