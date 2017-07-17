using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoogleRecaptcha.Extensions;
using GoogleRecaptcha.Models;
using GoogleRecaptcha.Services;
using Microsoft.Owin;
using Microsoft.Owin.Testing;
using Moq;
using NUnit.Framework;
using Owin;

namespace GoogleRecaptcha.UnitTests
{
    [TestFixture]
    public class GoogleRecaptchaMiddlewareTests
    {
        [Test]
        public void Should_throw_exception_when_site_secret_is_not_provided()
        {
            Assert.Throws<ArgumentException>(() => new GoogleRecaptchaMiddleware(It.IsAny<OwinMiddleware>(), new GoogleRecaptchaMiddlewareOption()
            {
                SiteSecret = null
            }));
        }
        
        [Test]
        public async Task Should_receive_unauthorized_when_google_recaptcha_response_token_is_invalid()
        {
            var httpClientMock = new Mock<IHttpClient>();
            httpClientMock
                .Setup( client =>  client.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>()))
                .ReturnsAsync(() => new GoogleRecaptchaResponse
                {
                    IsSuccessStatusCode = true,
                    ResponseContent = new GoogleRecaptchaResponseContent()
                    {
                        Success = false,
                        ErrorCodes = new []{ "invalid-input-response" }
                    }
                });


            using (var server = TestServer.Create(app =>
            {
                app.UseGoogleRecaptchaMiddleware(new GoogleRecaptchaMiddlewareOption()
                {
                    SiteSecret = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe",
                    BackchannelHttpClient = httpClientMock.Object
                });

                app.Run(async context =>
                {
                    await context.Response.WriteAsync("OK");
                });
            }))
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var content = new StringContent("g-recaptcha-response=test", Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await client.PostAsync("http://testserver/api/values", content);
                    var result = await response.Content.ReadAsStringAsync();

                    Assert.AreNotEqual(result, "OK");
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
                }
            }
        }


        [Test]
        public async Task Should_raise_InvalidInputSecretNotification_when_google_recaptcha_site_secret_is_invalid()
        {
            bool hasCalledInvalidInputSecretNotification = false;

            using (var server = TestServer.Create(app =>
            {
                app.UseGoogleRecaptchaMiddleware(new GoogleRecaptchaMiddlewareOption()
                {
                    SiteSecret = "test",
                    Notifications = new DefaultGoogleRecaptchaNotifications()
                    {
                        InvalidInputSecretNotification = async (c, r) =>
                        {
                            hasCalledInvalidInputSecretNotification = true;
                            await Task.FromResult(0);
                        }
                    }
                });

                app.Run(async context =>
                {
                    await context.Response.WriteAsync("OK");
                });
            }))
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var content = new StringContent("g-recaptcha-response=test", Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await client.PostAsync("http://testserver/api/values", content);

                    Assert.IsTrue(hasCalledInvalidInputSecretNotification);
                }
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_continue_to_next_middleware_based_on_return_configured_value(bool shouldContinue)
        {
            using (var server = TestServer.Create(app =>
            {
                app.UseGoogleRecaptchaMiddleware(new GoogleRecaptchaMiddlewareOption()
                {
                    SiteSecret = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe",
                    ShouldContinue = async response =>
                    {
                        return await Task.FromResult(shouldContinue);
                    },
                });

                app.Run(async context =>
                {
                    await context.Response.WriteAsync("OK");
                });
            }))
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var content = new StringContent("g-recaptcha-response=test", Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await client.PostAsync("http://testserver/api/values", content);
                    var result = await response.Content.ReadAsStringAsync();
                    if (shouldContinue)
                    {
                        Assert.AreEqual(result, "OK");
                    }
                    else
                    {
                        Assert.AreNotEqual(result, "OK");
                    }
                }
            }
        }
    }
}
