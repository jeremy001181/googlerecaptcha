using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GoogleRecaptcha.Extensions;
using GoogleRecaptcha.Models;
using GoogleRecaptcha.Services;
using Microsoft.Owin;

namespace GoogleRecaptcha
{
    public class GoogleRecaptchaMiddleware : OwinMiddleware
    {
        private readonly GoogleRecaptchaMiddlewareOption option;

        public GoogleRecaptchaMiddleware(OwinMiddleware next, GoogleRecaptchaMiddlewareOption option)
            : base(next)
        {
            SimpleContract.ThrowWhen<ArgumentNullException>(option == null, "option");
            SimpleContract.ThrowWhen<ArgumentException>(string.IsNullOrEmpty(option.SiteSecret), "siteSecret is null or empty");

            if (option.BackchannelHttpClient == null)
            {
                option.BackchannelHttpClient = new DefaultBackchannelHttpClient(new HttpClient(new HttpClientHandler()
                {
                    UseProxy = option.Proxy != null,
                    Proxy = option.Proxy
                })
                {
                    Timeout = new TimeSpan(0, 0, 0, option.Timeout ?? 30)
                });
            }

            if (option.GoogleRecaptchaRequestConstructor == null)
            {
                option.GoogleRecaptchaRequestConstructor = DefaultGoogleRecaptchaRequestConstructor;
            }

            if (option.GoogleRecaptchaResponseHandler == null)
            {
                option.GoogleRecaptchaResponseHandler = new DefaultGoogleRecaptchaResponseHandler(option.Notifications);
            }

            if (option.ShouldContinue == null)
            {
                option.ShouldContinue = DefaultShouldContinueHandler;
            }

            if (option.Enable == null)
            {
                option.Enable = DefaultEnabler;
            }

            this.option = option;
        }

        private async Task<bool> DefaultEnabler(IOwinContext context)
        {
            var isEnabled = context.Request.Method.Equals("POST");
            return await Task.FromResult(isEnabled);
        }

        private async Task<GoogleRecaptchaRequest> DefaultGoogleRecaptchaRequestConstructor(IOwinContext context)
        {
            var form = await context.Request.ReadFormFromBeginningAsync();

            return await Task.FromResult(new GoogleRecaptchaRequest(form["g-recaptcha-response"])
            {
                RemoteIp = context.Request.RemoteIpAddress
            });
        }

        private async Task<bool> DefaultShouldContinueHandler(GoogleRecaptchaResponse googleRecaptchaResponse)
        {
            return await Task.FromResult(googleRecaptchaResponse.IsSuccessStatusCode && googleRecaptchaResponse.ResponseContent.Success);
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (!await option.Enable(context))
            {
                await Next.Invoke(context);
                return;
            }

            var httpClient = option.BackchannelHttpClient;

            var request = await option.GoogleRecaptchaRequestConstructor(context);

            var data = string.Format("secret={0}&remoteip={1}&response={2}",
                HttpUtility.UrlEncode(option.SiteSecret),
                HttpUtility.UrlEncode(request.RemoteIp),
                HttpUtility.UrlEncode(request.UserResponseToken));

            var httpContent = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

            var googleRecaptchaResponse = await httpClient.PostAsync(option.TokenVerificationEndpoint, httpContent);
            
            await option.GoogleRecaptchaResponseHandler.HandleAsync(context, googleRecaptchaResponse);

            if (await option.ShouldContinue(googleRecaptchaResponse))
            {
                await Next.Invoke(context);
            }
        }
    }
}