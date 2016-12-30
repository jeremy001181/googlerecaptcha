using Owin;

namespace GoogleRecaptcha
{
    public static class GoogleRecaptchaMiddlewareExtensions
    {
        public static IAppBuilder UseGoogleRecaptchaMiddleware(this IAppBuilder appBuilder, GoogleRecaptchaMiddlewareOption option = null)
        {
            if (option == null)
            {
                option = new GoogleRecaptchaMiddlewareOption
                {
                    SiteKey = "",
                    SiteSecret = ""
                };
            }

            appBuilder.Use<GoogleRecaptchaMiddleware>(option);

            return appBuilder;
        }
    }
}