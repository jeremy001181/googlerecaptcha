using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public interface IGoogleRecaptchaResponseHandler
    {
        Task HandleAsync(IOwinContext context, GoogleRecaptchaResponse result);
        Task OwinContextPreInvoke(IOwinContext context);
    }
}