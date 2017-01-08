using System;
using Newtonsoft.Json;

namespace GoogleRecaptcha.Models
{
    public class GoogleRecaptchaResponseContent
    {
        public bool Success { get; set; }

        /// <summary>
        /// Optional. Timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
        /// </summary>
        [JsonProperty("challenge_ts")]
        public DateTime? ChallengeTimestamp { get; set; }

        /// <summary>
        /// The hostname of the site where the reCAPTCHA was solved
        /// </summary>
        public string HostName { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}