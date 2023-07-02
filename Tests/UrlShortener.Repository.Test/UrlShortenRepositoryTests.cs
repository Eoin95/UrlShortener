using Microsoft.EntityFrameworkCore;
using Moq;
using UrlShortener.Common.Models.Data;
using UrlShortener.Repository.Data;

namespace UrlShortener.Repository.Test {

    [TestFixture]
    public class UrlShortenRepositoryTests {

        private UrlShortenRepository urlShortenRepository;
        private Mock<IUrlShortenContext> mockUrlShortenContext;
        private UrlPair testUrlPair = new UrlPair {
            UrlPairID = 1,
            LongUrl = "testLongUrl",
            ShortUrl = "testShortUrl",
            CreatedDateTime = new DateTime()
        };
        [SetUp]
        public void Setup() {
            mockUrlShortenContext = new Mock<IUrlShortenContext>();
            urlShortenRepository = new UrlShortenRepository(mockUrlShortenContext.Object);
        }

        #region FindExistingTests
        [Test]
        public void FindExistingShortUrl_Success() {
            SetUpSuccessfulFindMock();

            var response = urlShortenRepository.FindExistingShortUrlFromLong(testUrlPair.LongUrl);

            Assert.IsNotNull(response);
            Assert.That(testUrlPair.ShortUrl, Is.EqualTo(response));
        }

        [Test]
        public void FindExistingShortUrl_NotFound() {
            SetUpUnSuccessfulFindMock();
            var response = urlShortenRepository.FindExistingShortUrlFromLong(testUrlPair.LongUrl);

            Assert.That("", Is.EqualTo(response));
        }

        [Test]
        public void FindExistingLongUrl_Success() {
            SetUpSuccessfulFindMock();

            var response = urlShortenRepository.FindExistingLongUrlFromShort(testUrlPair.ShortUrl);

            Assert.IsNotNull(response);
            Assert.That(testUrlPair.LongUrl, Is.EqualTo(response));
        }

        [Test]
        public void FindExistingLongUrl_NotFound() {
            SetUpUnSuccessfulFindMock();
            var response = urlShortenRepository.FindExistingLongUrlFromShort(testUrlPair.ShortUrl);

            Assert.That("", Is.EqualTo(response));
        }
        #endregion

        #region InsertTests

        [Test]
        public void InsertNewUrlPair() {
            var mockSet = new Mock<DbSet<UrlPair>>();
            mockUrlShortenContext.Setup(x => x.urlPairs).Returns(mockSet.Object);

            urlShortenRepository.InsertNewUrlPair(testUrlPair.ShortUrl, testUrlPair.LongUrl);

            mockSet.Verify(x => x.Add(It.IsAny<UrlPair>()), Times.Once());
            mockUrlShortenContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        #endregion

        #region MockSetups
        private void SetUpSuccessfulFindMock() {
            var testList = new List<UrlPair> { testUrlPair }.AsQueryable();
            SetUpFindMock(testList);
        }

        private void SetUpUnSuccessfulFindMock() {
            var testList = new List<UrlPair> { }.AsQueryable();
            SetUpFindMock(testList);
        }

        private void SetUpFindMock(IQueryable<UrlPair> testList) {
            var mockSet = new Mock<DbSet<UrlPair>>();
            mockSet.As<IQueryable<UrlPair>>().Setup(m => m.Provider).Returns(testList.Provider);
            mockSet.As<IQueryable<UrlPair>>().Setup(m => m.Expression).Returns(testList.Expression);
            mockSet.As<IQueryable<UrlPair>>().Setup(m => m.ElementType).Returns(testList.ElementType);
            mockSet.As<IQueryable<UrlPair>>().Setup(m => m.GetEnumerator()).Returns(() => testList.GetEnumerator());
            mockUrlShortenContext.Setup(x => x.urlPairs).Returns(mockSet.Object);
        }

        #endregion
    }
}