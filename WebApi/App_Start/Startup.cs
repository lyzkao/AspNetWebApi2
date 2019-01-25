using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using NSwag.AspNet.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApi.App_Start.Startup))]

namespace WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute("Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });

            app.UseSwaggerUi3(typeof(Startup).Assembly, settings =>
            {

            });
            app.UseWebApi(config);

            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
    }
}
