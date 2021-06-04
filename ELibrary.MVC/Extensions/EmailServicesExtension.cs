using ELibrary.Models.AppsettingModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.MVC.Extensions
{
    public static class EmailServicesExtension
    {

        public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("EmailSettings");
            services.Configure<EmailSettings>(section);
        }

    }
    
}
