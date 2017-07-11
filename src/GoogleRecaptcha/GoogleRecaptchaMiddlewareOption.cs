using System;
using System.Net;
using System.Threading.Tasks;
using GoogleRecaptcha.Models;
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

        /// <summary>
        /// Required. site secret.
        /// </summary>
        public string SiteSecret { get; set; }

        /// <summary>
        /// Optional. Google reCAPTCHA API endpoint for verification. If not specified, it is default to https://www.google.com/recaptcha/api/siteverify.
        /// </summary>
        public string TokenVerificationEndpoint { get; set; }

        /// <summary>
        /// Optional. Allow customize timeout (seconds) for built-in default backchannel HttpClient, default to 30 seconds.
        /// </summary>
        public int? Timeout { get; set; }
        
        /// <summary>
        /// Optional. Allow configure proxy for built-in default backchannel HttpClient.
        /// </summary>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// Optional. Allow customize how to interact with Google reCAPTCHA API endpoint.
        /// </summary> 
        public IHttpClient BackchannelHttpClient { get; set; }

        /// <summary>
        /// Optional. Preprocessing of Owin context prior to recatpcha response verification.
        /// </summary>
        public Func<IOwinContext, Task> OwinContextPreInvoke { get; set; }
        
        /// <summary>
        /// Optional. Allow customize how to handle Google recaptcha verfication response.
        /// </summary>
        public IGoogleRecaptchaResponseHandler GoogleRecaptchaResponseHandler { get; set; }

        /// <summary>
        /// Optional. Indicates if it should move to next middleware. If not specified, it only moves to next middleware when google recaptcha response is success.
        /// </summary>
        public Func<GoogleRecaptchaResponse, Task<bool>> ShouldContinue { get; set; }

        /// <summary>
        /// Optional. Allows customize how to extract goolgle recaptcha token and remote ip.
        /// </summary>
        public Func<IOwinContext, Task<GoogleRecaptchaRequest>> GoogleRecaptchaRequestConstructor { get; set; }

        /// <summary>
        /// Optional. Allow enable or disable google recaptcha verification in fly, default to be enabled if it is a POST request
        /// </summary>
        public Func<IOwinContext, Task<bool>> Enable { get; set; }
        
        /// <summary>
        /// Optional. Allow receive notifications from default google response handler
        /// </summary>
        public IGoogleRecaptchaNotifications Notifications { get; set; }
    }
}