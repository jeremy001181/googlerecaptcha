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
        }

        private Task DefaultInvalidInputResponseNotification(IOwinContext context, GoogleRecaptchaResponse googleRecaptchaResponse)
        {
            throw new NotImplementedException();
        }

        private Task DefaultMissingInputResponseNotification(IOwinContext context, GoogleRecaptchaResponse googleRecaptchaResponse)
        {
            throw new NotImplementedException();
        }

        public Func<IOwinContext, GoogleRecaptchaResponse, Task> InvalidInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> MissingInputResponseNotification { get; set; }
        public Func<IOwinContext, GoogleRecaptchaResponse, Task> ValidInputResponseNotification { get; set; }
    }

    public interface IGoogleRecaptchaNotifications
    {
        Func<IOwinContext, GoogleRecaptchaResponse, Task> InvalidInputResponseNotification { get; set; }
        Func<IOwinContext, GoogleRecaptchaResponse, Task> MissingInputResponseNotification { get; set; }
        Func<IOwinContext, GoogleRecaptchaResponse, Task> ValidInputResponseNotification { get; set; }
    }
}