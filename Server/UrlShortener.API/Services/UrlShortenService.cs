using Microsoft.Extensions.Options;
using UrlShortener.Common.Models;
using UrlShortener.Common.Models.Requests;
using UrlShortener.Common.Models.Responses;
using UrlShortener.Repository;

namespace UrlShortener.API.Services {
    public class UrlShortenService : IUrlShortenService {

        private IUrlShortenRepository _urlShortenRepository;
        private UrlGenerationOptions _urlGenerationOptions;
        public UrlShortenService(IUrlShortenRepository urlShortenRepository, IOptions<UrlGenerationOptions> urlGenerationOptions) {
            _urlShortenRepository = urlShortenRepository;
            _urlGenerationOptions = urlGenerationOptions.Value;
        }

        public async Task<FindUrlResponse> FindLongUrlFromShort(string shortUrl) {
            var response = new FindUrlResponse();
            var existingLongUrl = _urlShortenRepository.FindExistingLongUrlFromShort(shortUrl);
            if(!string.IsNullOrEmpty(existingLongUrl)) {
                response.Url = existingLongUrl;
                return response;
            }
            return response;
        }

        public async Task<string> UrlShorten(ShortenUrlRequest shortenUrlRequest) {
            var longUrl = shortenUrlRequest.LongUrl;
            var existingShortUrlId = _urlShortenRepository.FindExistingShortUrlFromLong(longUrl);
            if (!string.IsNullOrEmpty(existingShortUrlId)) {
                return existingShortUrlId;
            }

            var urlId = Nanoid.Nanoid.Generate(_urlGenerationOptions.Characters, _urlGenerationOptions.Length);

            _urlShortenRepository.InsertNewUrlPair(urlId, longUrl);

            return urlId;

        }
    }
}
