using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public class DefaultGoogleRecaptchaNotifications : IGoogleRecaptchaNotifications
    {
        public DefaultGoogleRecaptchaNotifications()
        {
            InvalidInputResponseNotification = DefaultInvalidInputResponseNotification;
            MissingInputResponseNotification = DefaultMissingInputResponseNotification;
            ValidInputResponseNotification = DefaultValidInputResponseNotification;
            InvalidInputSecretNotification = DefaultInvalidInputSecretNotification;
        }

        private async Task DefaultInvalidInputSecretNotification(IOwinContext arg1, GoogleRecaptchaResponse arg2)
        {
            await Task.FromResult(0);
        }

        private async Task DefaultValidInputResponseNotification(IOwinContext context, GoogleRecaptchaResponse googleRecaptchaResponse)
        {
            await Task.FromResult(0);
        }

        private async Task DefaultInvalidInputResponseNotification(IOwinContext context, GoogleRecaptchaResponse googleRecaptchaResponse)
        {
            await Task.FromResult(0);
        }

        private async Task DefaultMissingInputResponseNotification(IOwinContext context, GoogleRecaptchaResponse googleRecaptchaResponse)
        {
            await Task.FromResult(0);
        }

        public Func<IOwinContext, GoogleRecaptchaResponse, Task> InvalidInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> MissingInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> ValidInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> InvalidInputSecretNotification { get; set; }
    }
}