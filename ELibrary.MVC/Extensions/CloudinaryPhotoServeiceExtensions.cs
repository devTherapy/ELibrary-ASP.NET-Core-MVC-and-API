using ELibrary.Models.AppsettingModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.MVC.Extensions
{
    public static class CloudinaryPhotoServeiceExtensions
    {
        public static void AddCloudinaryPhotoConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("PhotoSettings");
            services.Configure<PhotoSettings>(section);
        }
    }
}
