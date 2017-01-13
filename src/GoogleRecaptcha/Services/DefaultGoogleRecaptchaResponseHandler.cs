using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public class DefaultGoogleRecaptchaResponseHandler : IGoogleRecaptchaResponseHandler
    {
        private readonly IGoogleRecaptchaNotifications notifications;

        public DefaultGoogleRecaptchaResponseHandler(IGoogleRecaptchaNotifications notifications)
        {
            this.notifications = notifications;
        }

        public async Task HandleAsync(IOwinContext context, GoogleRecaptchaResponse result)
        {
            if (!result.IsSuccessStatusCode || !result.ResponseContent.Success)
            {
                context.Response.StatusCode = 401;
            }

            if (notifications != null)
            {
                await notifications.PublishAsync(context, result);
            }
        }
    }
}