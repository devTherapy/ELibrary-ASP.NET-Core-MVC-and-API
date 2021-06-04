using ELibrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ELibrary.Data
{
    public static class LibrarySeeder
    {
      
       
        public static async Task ELibararyDbContext(ELibraryDbContext context, UserManager<AppUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {

            context.Database.EnsureCreated();
            await SeedRole(roleManager);
            await SeedUser(userManager, context);
            SeedCategory(context);
            SeedBooks(context);
            SeedReviews(context, userManager);
            SeedRating(context, userManager);
           
        }

       
        private static async Task SeedUser(UserManager<AppUser> userManager, ELibraryDbContext context)
        {
            if (!context.Users.Any())
            {
                var appUsersStream = await File.ReadAllTextAsync("../ELibrary.Data/JsonFiles/Users.json");
                var appUsers = JsonSerializer.Deserialize<IEnumerable<AppUser>>(appUsersStream);
                foreach (var appUser in appUsers)
                {
                    if (await userManager.FindByEmailAsync(appUser.Email) == null)
                    {
                        var newUser = new AppUser
                        {
                            Email = appUser.Email,
                            PhoneNumber = appUser.PhoneNumber,
                            UserName = appUser.Email,
                            FirstName = appUser.FirstName,
                            LastName = appUser.LastName,
                            PhotoUrl = appUser.PhotoUrl,

                        };

                        IdentityResult result = await userManager.CreateAsync(newUser, "Pa$$Word123");
                        if (result.Succeeded)
                        {
                            if ((await userManager .GetUsersInRoleAsync("Admin")).Count == 0)
                            {
                                await userManager.AddToRoleAsync(newUser, "Admin");
                            }
                            else
                            {
                                await userManager .AddToRoleAsync(newUser, "Regular");
                            }


                        }

                    }
                }
            }
          
           
          
            
        }

        public static void  SeedCategory(ELibraryDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categoriesStream = File.ReadAllText("../ELibrary.Data/JsonFiles/Category.json");
                var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(categoriesStream);

         
                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                context.SaveChanges();
            }
           
        }

        public static void SeedBooks(ELibraryDbContext context)
        {
            if (!context.Books.Any())
            {
                var bookStreams = File.ReadAllText("../ELibrary.Data/JsonFiles/Books.json");
                var books = JsonSerializer.Deserialize<IEnumerable<Book>>(bookStreams);
                foreach (var book in books)
                {
                    book.AddedDate = Convert.ToDateTime(book.AddedDate);
                    book.PublishedDate = Convert.ToDateTime(book.PublishedDate);
                    context.Books.Add(book);
                }
                context.SaveChanges();
            }
           
        }

        public static void SeedReviews(ELibraryDbContext context, UserManager<AppUser> userManager)
        {
            if (!context.Reviews.Any())
            {
                int count = 0;
                var users = userManager.Users.ToList();
               
                var reviewsStreams = File.ReadAllText("../ELibrary.Data/JsonFiles/Review.json");
                var reviews = JsonSerializer.Deserialize<IEnumerable<Review>>(reviewsStreams);
                foreach (var review in reviews)
                {
                    review.CreatedAt = Convert.ToDateTime(review.CreatedAt);
                    review.AppUserId = users[count].Id;
                    context.Reviews.Add(review);

                    if (count >= users.Count - 1)
                    {
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }
                    
                }
                context.SaveChanges();
            }
           
        }
        public static void SeedRating(ELibraryDbContext context, UserManager<AppUser> userManager)
        {
            if (!context.Ratings.Any())
            {
                int count = 0;
                var users = userManager.Users.ToList();
                var ratingStreams = File.ReadAllText("../ELibrary.Data/JsonFiles/Rating.json");
                var ratings = JsonSerializer.Deserialize<IEnumerable<Rating>>(ratingStreams);
                foreach (var rating in ratings)
                {
                   
                    rating.AppUserId = users[count].Id;
                    context.Ratings.Add(rating);
                    
                    if (count >= users.Count - 1)
                    {
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }

                }
                context.SaveChanges();
            }
           
        }
        public static async Task SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                if (await roleManager.RoleExistsAsync("Regular") == false)
                {
                    var role = new IdentityRole
                    {
                        Name = "Regular",


                    };
                    await roleManager.CreateAsync(role);
                }

                if (await roleManager.RoleExistsAsync("Admin") == false)
                {
                    var role = new IdentityRole
                    {
                        Name = "Admin"
                    };

                    await roleManager.CreateAsync(role);
                }
            }
          
        }

    }
}
