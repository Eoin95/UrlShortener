namespace UrlShortener.Repository {
    public interface IUrlShortenRepository {

        string FindExistingLongUrlFromShort(string urltoSearch);

        string FindExistingShortUrlFromLong(string urltoSearch);

        void InsertNewUrlPair(string shortUrl, string longUrl);
    }
}