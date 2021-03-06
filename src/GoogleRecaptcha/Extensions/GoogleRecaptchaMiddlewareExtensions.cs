﻿using Owin;

namespace GoogleRecaptcha.Extensions
{
    public static class GoogleRecaptchaMiddlewareExtensions
    {
        public static IAppBuilder UseGoogleRecaptchaMiddleware(this IAppBuilder appBuilder, GoogleRecaptchaMiddlewareOption option)
        {
            appBuilder.Use<GoogleRecaptchaMiddleware>(option);

            return appBuilder;
        }
    }
}