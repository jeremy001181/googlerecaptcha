using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoogleRecaptcha.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static async Task<T> ReadAsJsonObjectAsync<T>(this HttpContent httpContent)
        {
            var content = await httpContent.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
