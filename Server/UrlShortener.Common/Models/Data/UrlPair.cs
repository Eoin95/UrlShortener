namespace UrlShortener.Common.Models.Data {
    public class UrlPair {

        public int UrlPairID { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }

    }
}
