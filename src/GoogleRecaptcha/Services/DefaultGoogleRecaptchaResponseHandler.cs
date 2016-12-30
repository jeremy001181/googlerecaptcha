using System.Threading.Tasks;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public class DefaultGoogleRecaptchaResponseHandler : IGoogleRecaptchaResponseHandler
    {
        public async Task Handle(IOwinContext context, GoogleRecaptchaResponse result)
        {
            if (!result.Success)
            {
                context.Response.StatusCode = 401;
                
                await context.Response.WriteAsync("rejected");
            }
            else
            {

                context.Response.StatusCode = 200;

                await context.Response.WriteAsync("success");
                //await option.Notifications.InvalidInputResponseNotification(context, result);
            }
        }
    }
}