using ELibrary.Models.AppsettingModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.MVC.Extensions
{
    public static class CloudinaryServiceExtension
    {
        public static void AddCloudinaryConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("CloudinarySettings");
            services.Configure<CloudinarySettings>(section);
        }
    }
}
