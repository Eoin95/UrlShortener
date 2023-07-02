using Microsoft.EntityFrameworkCore;
using UrlShortener.Common.Models.Data;

namespace UrlShortener.Repository.Data {
    public class UrlShortenContext : DbContext, IUrlShortenContext {

        public UrlShortenContext(DbContextOptions<UrlShortenContext> options) : base(options) { 
        
        }

        public DbSet<UrlPair> urlPairs { get; set; }

    }
}
