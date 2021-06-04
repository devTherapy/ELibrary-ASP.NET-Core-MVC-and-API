using ELibrary.Core.Abstractions;
using ELibrary.Core.Implementations;
using ELibrary.Data;
using ELibrary.Models;
using ELibrary.MVC.ExceptionExtension;
using ELibrary.MVC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Data.Repositories.Implementations;
using System;
using Microsoft.AspNetCore.Http;

namespace ELibrary.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddJwtAuth(Configuration);
            services.AddDependencyInjection();

            services.AddDbContextPool<ELibraryDbContext>
                (option => option.UseSqlite(Configuration.GetConnectionString("Default")));



            //Identity Setup
            services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                    //options.SignIn.RequireConfirmedEmail = true;

                }
                ).AddEntityFrameworkStores<ELibraryDbContext>().AddDefaultTokenProviders();
            services.AddEmailConfiguration(Configuration);
            services.AddCloudinaryConfiguration(Configuration);
            services.AddCloudinaryPhotoConfiguration(Configuration);
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<ICloudinaryServices, CloudinaryServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IRepository<Book>, BookRepository>();
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ELibraryDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseMiddleware<ExceptionMiddleWare>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.Use(async (ctx, next) =>
            {
                var Token = ctx.Session.GetString("Token");
                if (!string.IsNullOrEmpty(Token))
                {
                    ctx.Request.Headers.Add("Authorization", "Bearer " + Token);
                }
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            LibrarySeeder.ELibararyDbContext(context, userManager, roleManager).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
