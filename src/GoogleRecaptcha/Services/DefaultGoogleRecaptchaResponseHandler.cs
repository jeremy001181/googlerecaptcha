using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public class DefaultGoogleRecaptchaResponseHandler : IGoogleRecaptchaResponseHandler
    {
        public async Task Handle(IOwinContext context, GoogleRecaptchaResponse result)
        {
            if (!result.IsSuccessStatusCode || !result.ResponseContent.Success)
            {
                context.Response.StatusCode = 401;
            }

            // Followings are all about notifications
            if (Notifications == null)
            {
                return;
            }

            if (!result.IsSuccessStatusCode)
            {
                if (Notifications.FailedResponseNotification != null)
                {
                    await Notifications.FailedResponseNotification(context, result);
                }

                return;
            }

            if (result.ResponseContent.Success)
            {
                // Google verfication passed
                if (Notifications.ValidInputResponseNotification != null)
                {
                    await Notifications.ValidInputResponseNotification(context, result);
                }
            }
            else
            {
                // Google verfication failed
                foreach (var errorCode in result.ResponseContent.ErrorCodes)
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

        public IGoogleRecaptchaNotifications Notifications { get; set; }
    }
}