using Microsoft.EntityFrameworkCore;
using UrlShortener.Common.Models.Data;

namespace UrlShortener.Repository.Data {
    public interface IUrlShortenContext : IDisposable {

        int SaveChanges();
        public DbSet<UrlPair> urlPairs { get; set; }
    }
}
