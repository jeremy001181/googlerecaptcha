using System.Net.Http;
using System.Threading.Tasks;
using GoogleRecaptcha.Models;

namespace GoogleRecaptcha.Services
{
    public interface IHttpClient
    {
        Task<GoogleRecaptchaResponse> PostAsync(string uri, HttpContent content);
    }
}