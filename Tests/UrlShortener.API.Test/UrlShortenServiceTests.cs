using Microsoft.Extensions.Options;
using Moq;
using System.Text.RegularExpressions;
using UrlShortener.API.Services;
using UrlShortener.Common.Models;
using UrlShortener.Common.Models.Requests;
using UrlShortener.Repository;

namespace UrlShortener.API.Test {
    [TestFixture]
    public class UrlShortenServiceTests {

        private Mock<IUrlShortenRepository> mockUrlShortenRepository;
        private IOptions<UrlGenerationOptions> options;
        private UrlShortenService urlShortenService;
        private UrlGenerationOptions urlGenerationOptions = new UrlGenerationOptions { Length = 15, Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" };
        private Regex validator;

        [SetUp]
        public void Setup() {
            mockUrlShortenRepository = new Mock<IUrlShortenRepository>();
            options = Options.Create<UrlGenerationOptions>(urlGenerationOptions);
            validator = new Regex($@"^[{urlGenerationOptions.Characters}]+$");
            urlShortenService = new UrlShortenService(mockUrlShortenRepository.Object, options);
        }

        [Test]
        [TestCase("ExistingUrl", "ResponseUrl")]
        [TestCase("NotExistingUrl", null)]
        public async Task FindLongUrlFromShort(string shortUrl, string expectedResponseUrl) {
            mockUrlShortenRepository.Setup(x => x.FindExistingLongUrlFromShort(shortUrl)).Returns(expectedResponseUrl);

            var response = await urlShortenService.FindLongUrlFromShort(shortUrl);
            Assert.IsNotNull(response);
            Assert.That(expectedResponseUrl, Is.EqualTo(response.Url));
        }


        [Test]
        [TestCase("NewUrl", null)]
        [TestCase("ExistingUrl", "ResponseUrl")]
        public async Task ShortenUrlSuccess(string inputUrl, string expectedResponseUrl) {
            mockUrlShortenRepository.Setup(x => x.FindExistingShortUrlFromLong(inputUrl)).Returns(expectedResponseUrl);
            mockUrlShortenRepository.Setup(x => x.InsertNewUrlPair(inputUrl, It.IsAny<string>()));
            var request = new ShortenUrlRequest { LongUrl = inputUrl };

            var response = await urlShortenService.UrlShorten(request);
            Assert.IsNotNull(response);
            if (!string.IsNullOrEmpty(expectedResponseUrl)) {
                Assert.That(expectedResponseUrl, Is.EqualTo(response));
                mockUrlShortenRepository.Verify(x => x.InsertNewUrlPair(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            }
            else {
                Assert.That(urlGenerationOptions.Length, Is.EqualTo(response.Length));
                Assert.True(validator.IsMatch(response));
            }
        }
    }
}
