# Owin middleware for Google Recaptcha v2

This middleware captures user reponse token and post to Google recaptcha verification API. 

###Basic

    var option = new GoogleRecaptchaMiddlewareOption()
    {        
        SiteSecret = "your-site-secret", // always required
    };

    app.UseGoogleRecaptchaMiddleware(option);

###Or customize your own one ...

    var option = new GoogleRecaptchaMiddlewareOption()
    {
        SiteSecret = "your-site-secret",
        Notifications = new DefaultGoogleRecaptchaNotifications
        {
            FailedResponseNotification = async (owinContext, response) => { 
                /*... triggered when google returns failure HTTP status code ...*/ 
            }, 
            InvalidInputResponseNotification = async (owinContext, response) => { 
                /*... triggered when failed to validate google recaptcha response token against google API ...*/ 
            }, 
            /*... you should be able to understand the rest notifications by looking at their names, I guess? ...*/ 
            MissingInputResponseNotification = async (owinContext, response) => { /*...do your thing..*/ }, 
            InvalidInputSecretNotification = async (owinContext, response) => { /*...do your thing..*/ },  
            ValidInputResponseNotification = async (owinContext, response) => { /*...do your thing..*/ }
        },
        ShouldContinue = async response => { /* indicates if it should move to next middleware */ },
        Enable = async owinContext => { /* allow enable or disable google reCAPTCHA verification in fly */ },     
        GoogleRecaptchaRequestConstructor = async owinContext => { /* how to extract goolgle reCAPTCHA token and remote ip */ }
    };    
    app.UseGoogleRecaptchaMiddleware(option);

###Or more customization...

1. `GoogleRecaptchaResponseHandler`, allow you to handle google recaptcha verification response in your own way.
2. `BackchannelHttpClient`, allow you customize how to interact with Google reCAPTCHA API endpoint.
