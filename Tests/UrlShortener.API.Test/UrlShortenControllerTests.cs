using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlShortener.API.Controllers;
using UrlShortener.API.Services;
using UrlShortener.Common.Models.Requests;
using UrlShortener.Common.Models.Responses;

namespace UrlShortener.API.Test {
    public class UrlShortenControllerTests {

        private Mock<IUrlShortenService> mockUrlShortenService;
        private UrlShortenController urlShortenController;

        [SetUp]
        public void Setup() {
            mockUrlShortenService = new Mock<IUrlShortenService>();
            urlShortenController = new UrlShortenController(mockUrlShortenService.Object);
        }

        [Test]
        [TestCase("ExistingUrl", "ResponseUrl")]
        [TestCase("NonexistingUrl", null)]
        public async Task FindExistingUrlTests(string inputUrl, string responseUrl) {

            mockUrlShortenService.Setup(x => x.FindLongUrlFromShort(inputUrl)).ReturnsAsync(new FindUrlResponse { Url = responseUrl });

            var response = await urlShortenController.FindExistingUrl(inputUrl);
            Assert.IsNotNull(response);
            if(responseUrl == null) {
                Assert.IsInstanceOf<NotFoundResult>(response);
            } else {
                Assert.IsInstanceOf<OkObjectResult>(response);
                var result = response as OkObjectResult;
                Assert.That(responseUrl, Is.EqualTo((result.Value as FindUrlResponse).Url));
            }
        }

        [Test]
        [TestCase("https://www.test.com", "validResult", true)]
        [TestCase("NotAUrl", null, false)]
        public async Task ShortenUrlTests(string input, string serviceResponse, bool expectValid) {
            mockUrlShortenService.Setup(x => x.UrlShorten(It.IsAny<ShortenUrlRequest>())).ReturnsAsync(serviceResponse);

            var response = await urlShortenController.ShortenUrl(new ShortenUrlRequest { LongUrl = input });
            Assert.IsNotNull(response);
            if (!expectValid) {
                Assert.IsInstanceOf<BadRequestObjectResult>(response);
                mockUrlShortenService.Verify(x => x.UrlShorten(It.IsAny<ShortenUrlRequest>()), Times.Never());
            } else {
                Assert.IsInstanceOf<OkObjectResult>(response);
                var result = response as OkObjectResult;
                Assert.That(serviceResponse, Is.EqualTo(result.Value));
            }
        }
    }
}