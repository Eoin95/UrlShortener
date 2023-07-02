using UrlShortener.Common.Models.Data;
using UrlShortener.Repository.Data;

namespace UrlShortener.Repository {
    public class UrlShortenRepository : IUrlShortenRepository {

        private IUrlShortenContext _dbContext;

        public UrlShortenRepository(IUrlShortenContext dbContext) {
            _dbContext = dbContext;
        }

        public string FindExistingLongUrlFromShort(string urltoSearch) {
            var foundPair = _dbContext.urlPairs.SingleOrDefault(x => x.ShortUrl.Equals(urltoSearch));
            return foundPair != null ? foundPair.LongUrl : "";
        }

        public string FindExistingShortUrlFromLong(string urltoSearch) {
            var foundPair = _dbContext.urlPairs.SingleOrDefault(x => x.LongUrl.Equals(urltoSearch));
            return foundPair != null ? foundPair.ShortUrl : "";
        }

        public void InsertNewUrlPair(string shortUrl, string longUrl) {
            _dbContext.urlPairs.Add(new UrlPair { ShortUrl = shortUrl, LongUrl = longUrl, CreatedDateTime = DateTime.Now });
            _dbContext.SaveChanges();
        }
    }
}
