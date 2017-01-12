using System;
using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public class DefaultGoogleRecaptchaNotifications : IGoogleRecaptchaNotifications
    {
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> InvalidInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> MissingInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> ValidInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> InvalidInputSecretNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> FailedResponseNotification { get; set; }

        public async Task PublishAsync(IOwinContext context, GoogleRecaptchaResponse result)
        {
            if (!result.IsSuccessStatusCode)
            {
                if (FailedResponseNotification != null)
                {
                    await FailedResponseNotification(context, result);
                }

                return;
            }

            if (result.ResponseContent.Success)
            {
                // Google verfication passed
                if (ValidInputResponseNotification != null)
                {
                    await ValidInputResponseNotification(context, result);
                }
            }
            else
            {
                // Google verfication failed
                foreach (var errorCode in result.ResponseContent.ErrorCodes)
                {
                    if (errorCode == "invalid-input-secret" && InvalidInputSecretNotification != null)
                        await InvalidInputSecretNotification(context, result);

                    if (errorCode == "missing-input-response" && MissingInputResponseNotification != null)
                        await MissingInputResponseNotification(context, result);

                    if (errorCode == "invalid-input-response" && InvalidInputResponseNotification != null)
                        await InvalidInputResponseNotification(context, result);
                }
            }
        }
    }
}