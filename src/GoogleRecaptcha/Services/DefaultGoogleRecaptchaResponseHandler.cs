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
            }

            if (Notifications != null)
            {
                if (result.Success)
                {
                    await Notifications.ValidInputResponseNotification(context, result);
                }
                else
                {
                    foreach (var errorCode in result.ErrorCodes)
                    {
                        if (errorCode == "invalid-input-secret" && Notifications.InvalidInputSecretNotification != null)
                            await Notifications.InvalidInputSecretNotification(context, result);

                        if (errorCode == "missing-input-response" && Notifications.MissingInputResponseNotification != null)
                            await Notifications.MissingInputResponseNotification(context, result);

                        if (errorCode == "invalid-input-response" && Notifications.InvalidInputResponseNotification != null)
                            await Notifications.InvalidInputResponseNotification(context, result);
                    }
                }
            }
        }

        public IGoogleRecaptchaNotifications Notifications { get; set; }
    }
}