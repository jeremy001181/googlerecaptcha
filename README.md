# Owin middleware for Google Recaptcha v2

This middleware captures user reponse token and post to Google recaptcha verification API. 

    var option = new GoogleRecaptchaMiddlewareOption()
    {
        SiteSecret = "your-site-secret",
    };

    app.UseGoogleRecaptchaMiddleware(option);
