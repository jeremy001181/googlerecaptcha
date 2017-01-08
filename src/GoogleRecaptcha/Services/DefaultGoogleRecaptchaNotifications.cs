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
    }
}