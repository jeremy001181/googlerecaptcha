using System.Configuration;
using System.Threading.Tasks;
using GoogleRecaptcha;
using GoogleRecaptcha.Services;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(MvcApplication.Startup))]

namespace MvcApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            const string rootFolder = ".";
            var fileSystem = new PhysicalFileSystem(rootFolder);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };

            app.UseFileServer(options);

            app.Map("/protected", appBuilder =>
            {
                var option = new GoogleRecaptchaMiddlewareOption()
                {
                    SiteSecret = ConfigurationManager.AppSettings["google.siteSecret"],
                };

                appBuilder.UseGoogleRecaptchaMiddleware(option);

                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<a href='/index.html'>Go back</a>");
                });
                
            });

        }
    }
}