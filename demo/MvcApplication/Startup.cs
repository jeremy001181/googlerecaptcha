using System.Threading.Tasks;
using GoogleRecaptcha;
using GoogleRecaptcha.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MvcApplication.Startup))]

namespace MvcApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                
                var option = new GoogleRecaptchaMiddlewareOption()
                {
                    Timeout = 60,
                    Notifications = new DefaultGoogleRecaptchaNotifications()
                    {
                        MissingInputResponseNotification = async (ctx, recaptchaResponse) =>
                        {
                            await Task.FromResult(0);
                        },
                        InvalidInputResponseNotification = async (ctx, recaptchaResponse) =>
                        {
                            await Task.FromResult(0);
                        }
                    }
                };
                
                app.UseGoogleRecaptchaMiddleware(option);

                return context.Response.WriteAsync("Hello, world.");
            });
        }
    }
}