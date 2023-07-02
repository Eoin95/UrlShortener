using UrlShortener.Common.Models.Requests;
using UrlShortener.Common.Models.Responses;

namespace UrlShortener.API.Services {
    public interface IUrlShortenService {

        Task<FindUrlResponse> FindLongUrlFromShort(string shortUrl);

        Task<string> UrlShorten(ShortenUrlRequest url);
    }
}
