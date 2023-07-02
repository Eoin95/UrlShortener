using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.Services;
using UrlShortener.Common.Models.Requests;

namespace UrlShortener.API.Controllers {
    [ApiController]
    [Route("shorten")]
    public class UrlShortenController : ControllerBase {

        private readonly IUrlShortenService _urlShortenService;

        public UrlShortenController(IUrlShortenService urlShortenService) {
            _urlShortenService = urlShortenService;
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public async Task<IActionResult> FindExistingUrl([FromRoute] string shortUrl) {
            var response = await _urlShortenService.FindLongUrlFromShort(shortUrl);
            if (string.IsNullOrEmpty(response.Url)) {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlRequest urlToShorten) {
            if (!Uri.IsWellFormedUriString(urlToShorten.LongUrl, UriKind.Absolute)) {
                return BadRequest("Not a valid Url");
            }
            return Ok(await _urlShortenService.UrlShorten(urlToShorten));
        }
    }
}