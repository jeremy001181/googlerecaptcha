using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GoogleRecaptcha.Extensions;
using GoogleRecaptcha.Services;
using Microsoft.Owin;

namespace GoogleRecaptcha
{
    public class GoogleRecaptchaMiddleware : OwinMiddleware
    {
        private readonly GoogleRecaptchaMiddlewareOption option;

        public GoogleRecaptchaMiddleware(OwinMiddleware next, GoogleRecaptchaMiddlewareOption option) : base(next)
        {
            SimpleContract.ThrowWhen<ArgumentNullException>(option == null, "option");
            SimpleContract.ThrowWhen<ArgumentException>(string.IsNullOrEmpty(option.SiteKey), "siteKey is null or empty");
            SimpleContract.ThrowWhen<ArgumentException>(string.IsNullOrEmpty(option.SiteSecret), "siteSecret is null or empty");
            
            if (option.BackchannelHttpClient == null)
            {
                option.BackchannelHttpClient = new DefaultBackchannelHttpClient
                {
                    Proxy = option.Proxy
                };
            }

            if (option.RequestConstructor == null)
            {
                option.RequestConstructor = DefaultGoogleRecaptchaRequestConstructor;
            }

            if (option.ResponseHandler == null)
            {
                option.ResponseHandler = new DefaultGoogleRecaptchaResponseHandler();
            }

            if (option.ShouldContinue == null)
            {
                option.ShouldContinue = DefaultShouldContinueHandler;
            }

            this.option = option;
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
            return await Task.FromResult(googleRecaptchaResponse.Success);
        }

        public async override Task Invoke(IOwinContext context)
        {
            var httpClient = option.BackchannelHttpClient;
            
            var request = await option.RequestConstructor(context);

            var data = string.Format(
                "secret={0}&remoteip={1}&response={2}",
                                    HttpUtility.UrlEncode(option.SiteSecret),
                                    HttpUtility.UrlEncode(request.RemoteIp),
                                    HttpUtility.UrlEncode(request.UserResponseToken));

            var httpContent = new StringContent(data, Encoding.UTF8 ,"application/x-www-form-urlencoded");

            GoogleRecaptchaResponse result;

            using (var response = await httpClient.PostAsync(option.TokenVerificationEndpoint,  httpContent))
            {
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadAsJsonObjectAsync<GoogleRecaptchaResponse>();
            }
            
            await option.ResponseHandler.Handle(context, result);

            if (await option.ShouldContinue(result))
            {
                await Next.Invoke(context);
            }
        }
    }
}