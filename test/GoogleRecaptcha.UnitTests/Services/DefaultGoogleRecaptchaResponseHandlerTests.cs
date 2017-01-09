using System.Net;
using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using GoogleRecaptcha.Services;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace GoogleRecaptcha.UnitTests.Services
{
    [TestFixture]
    class DefaultGoogleRecaptchaResponseHandlerTests
    {
        [Test]
        public async Task Should_raise_fail_response_notification_when_receives_response_with_failure_status_code()
        {
            bool actual = false;
            var contextMock = new Mock<IOwinContext>();
            contextMock.SetupGet(context => context.Response).Returns(Mock.Of<IOwinResponse>());
            var handler = new DefaultGoogleRecaptchaResponseHandler()
            {
                Notifications = new DefaultGoogleRecaptchaNotifications()
                {
                    FailedResponseNotification = async (context, response) =>
                    {
                        actual = true;
                        await Task.FromResult(0);
                    }
                }
            };

            await handler.Handle(contextMock.Object, new GoogleRecaptchaResponse()
            {
                IsSuccessStatusCode = false
            });

            Assert.AreEqual(true, actual);
        }
    }
}
