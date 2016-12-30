using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleRecaptcha.Services
{
    internal class DefaultBackchannelHttpClient : IHttpClient
    {
        private readonly HttpClient httpClient;

        internal DefaultBackchannelHttpClient()
        {
            this.httpClient = new HttpClient();
        }

        internal IWebProxy Proxy { get; set; }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            return await httpClient.PostAsync(uri, content);
        }
    }
}