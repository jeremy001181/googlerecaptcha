using System.Net.Http;
using System.Threading.Tasks;
using GoogleRecaptcha.Extensions;
using GoogleRecaptcha.Models;

namespace GoogleRecaptcha.Services
{
    internal class DefaultBackchannelHttpClient : IHttpClient
    {
        private readonly HttpClient httpClient;

        internal DefaultBackchannelHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GoogleRecaptchaResponse> PostAsync(string uri, HttpContent content)
        {
            var googleRecaptchaResponse = new GoogleRecaptchaResponse();

            try
            {
                using (var response = await httpClient.PostAsync(uri, content))
                {
                    googleRecaptchaResponse.StatusCode = response.StatusCode;
                    googleRecaptchaResponse.ReasonPhase = response.ReasonPhrase;
                    googleRecaptchaResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        googleRecaptchaResponse.ResponseContent = await response.Content.ReadAsJsonObjectAsync<GoogleRecaptchaResponseContent>();
                    }
                }

                return await Task.FromResult(googleRecaptchaResponse);
            }
            catch (HttpRequestException ex)
            {
                googleRecaptchaResponse.Exception = ex;
            }
            catch (TaskCanceledException ex)
            {

                googleRecaptchaResponse.Exception = ex;
            }

            return await Task.FromResult(googleRecaptchaResponse);
        }
    }
}