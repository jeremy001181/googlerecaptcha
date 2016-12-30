using System;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace GoogleRecaptcha.UnitTests
{
    [TestFixture]
    public class GoogleRecaptchaMiddlewareTests
    {
        [Test]
        public void Should_throw_exception_when_site_key_or_site_secret_is_not_provided()
        {
            Assert.Throws<ArgumentException>(() => new GoogleRecaptchaMiddleware(It.IsAny<OwinMiddleware>(), new GoogleRecaptchaMiddlewareOption()
            {
                SiteKey = null,
                SiteSecret = "test"
            }));

            Assert.Throws<ArgumentException>(() => new GoogleRecaptchaMiddleware(It.IsAny<OwinMiddleware>(), new GoogleRecaptchaMiddlewareOption()
            {
                SiteKey = "test",
                SiteSecret = null
            }));
        }
    }
}
