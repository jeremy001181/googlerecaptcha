using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleRecaptcha.Services
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
    }
}