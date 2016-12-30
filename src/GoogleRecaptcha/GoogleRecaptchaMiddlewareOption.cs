using System;
using System.Net;
using System.Threading.Tasks;
using GoogleRecaptcha.Services;
using Microsoft.Owin;

namespace GoogleRecaptcha
{
    public class GoogleRecaptchaMiddlewareOption
    {
        public GoogleRecaptchaMiddlewareOption()
        {
            TokenVerificationEndpoint = "https://www.google.com/recaptcha/api/siteverify";
        }

        public string SiteKey { get; set; }
        public string SiteSecret { get; set; }

        /// <summary>
        /// Optional. Google reCAPTCHA API endpoint for verification. If not specified, it is default to https://www.google.com/recaptcha/api/siteverify.
        /// </summary>
        public string TokenVerificationEndpoint { get; set; }

        public int Timeout { get; set; }
        public IGoogleRecaptchaNotifications Notifications { get; set; }
        public IWebProxy Proxy { get; set; }
        public IHttpClient BackchannelHttpClient { get; set; }
        public IGoogleRecaptchaResponseHandler ResponseHandler { get; set; }

        /// <summary>
        /// Optional. Indicates if it should move to next middleware. If not specified, it only moves to next middleware when google recaptcha response is success.
        /// </summary>
        public Func<GoogleRecaptchaResponse, Task<bool>> ShouldContinue { get; set; }

        /// <summary>
        /// Optional. Allows customize how to extract goolgle recaptcha token and remote ip.
        /// </summary>
        public Func<IOwinContext, Task<GoogleRecaptchaRequest>> RequestConstructor { get; set; }
    }
}