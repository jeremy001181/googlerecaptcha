namespace GoogleRecaptcha.Models
{
    public class GoogleRecaptchaRequest
    {
        public GoogleRecaptchaRequest(string userResponseToken)
        {
            UserResponseToken = userResponseToken;
        }
        /// <summary>
        /// Optional. The user's IP address.
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        /// Required. The user response token provided by reCAPTCHA, verifying the user on your site.
        /// </summary>
        public string UserResponseToken { get; private set; }
    }
}