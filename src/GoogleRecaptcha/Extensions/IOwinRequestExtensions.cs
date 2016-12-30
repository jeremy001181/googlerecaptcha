using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace GoogleRecaptcha.Extensions
{
    internal static class IOwinRequestExtensions
    {
        internal static async Task<IFormCollection> ReadFormFromBeginningAsync(this IOwinRequest request)
        {
            request.Body.Seek(0L, SeekOrigin.Begin);
            var form = await request.ReadFormAsync();
            request.Body.Seek(0L, SeekOrigin.Begin);

            return form;
        }
    }
}
