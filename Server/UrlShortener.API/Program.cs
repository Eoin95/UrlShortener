
using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Extensions;
using UrlShortener.API.Services;
using UrlShortener.Common.Models;
using UrlShortener.Repository;
using UrlShortener.Repository.Data;

namespace UrlShortener.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            var appConfigs = GetApplicationConfiguration(args);
            appConfigs = BuildDbConnectionString(appConfigs);

            // Add services to the container.
            builder.Services.AddScoped<IUrlShortenService, UrlShortenService>();
            builder.Services.AddScoped<IUrlShortenRepository, UrlShortenRepository>();
            builder.Services.AddScoped<IUrlShortenContext, UrlShortenContext>();
            builder.Services.Configure<UrlGenerationOptions>(appConfigs.GetSection(nameof(UrlGenerationOptions)));

            builder.Services.AddDbContext<IUrlShortenContext, UrlShortenContext>(options => options.UseSqlServer(appConfigs.GetConnectionString("UrlShortenContext")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddCors(o => o.AddDefaultPolicy( builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }

        private static IConfigurationRoot GetApplicationConfiguration(string[] args) {
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "Conf");
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            return new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile($"{appSettingsPath}/appsettings.{env}.json", true, false)
                .AddJsonFile($"{appSettingsPath}/appsettings.json", false, false)
                .AddJsonFile($"{appSettingsPath}/appsettings.secrets.json", false, true)
                .Build();
        }

        private static IConfigurationRoot BuildDbConnectionString(IConfigurationRoot appConfigs) {
            var connectionString = appConfigs.GetConnectionString("UrlShortenContext");
            var user = appConfigs.GetSection("UrlShortenDbSecret:UserId").Value;
            var password = appConfigs.GetSection("UrlShortenDbSecret:UserPassword").Value;
            connectionString = string.Format(connectionString, user, password);
            appConfigs.GetSection("ConnectionStrings:UrlShortenContext").Value = connectionString;
            return appConfigs;
        }
    }
}