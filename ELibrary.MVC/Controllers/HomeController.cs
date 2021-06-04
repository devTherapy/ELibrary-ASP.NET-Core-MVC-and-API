using ELibrary.Common.Helpers;
using ELibrary.Data.Repositories.Implementations;
using ELibrary.Dtos;
using ELibrary.Models;
using ELibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.MVC.Controllers
{
    public class HomeController : Controller
    {
        private const string BASE_URL = "https://localhost:44326/api/";
        private readonly ApiHttpClient _httpClient = new ApiHttpClient();

        public async Task<IActionResult> Index()
        {
            var bookUrl = BASE_URL + "book";
            var bookResponse = await _httpClient.Client.GetAsync(bookUrl);

            var deserializedBookResponseObject = JsonConvert.DeserializeObject<ResponseDto<Pagination<GetBookDto>>>(await bookResponse.Content.ReadAsStringAsync());
            var deserializedBookResponse = deserializedBookResponseObject.Data;
            var homeViewModel = new HomeViewModel();

            foreach (var bookDto in deserializedBookResponse)
            {
                var count = bookDto.Rate.Count();
                var totalRate = 0;

                if(count != 0)
                {
                    totalRate = bookDto.Rate.Sum(rating => rating.Rate) / count;
                }

                var book = new BookViewModel
                {
                    Id = bookDto.Id,
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Availability = bookDto.Availability,
                    PhotoUrl = bookDto.PhotoUrl,
                    Rating = totalRate
                };

                homeViewModel.Books.Add(book);
            }

            var mostpopularUrl = "book/sort-book-by-view";
            var newestUrl = "book/sort-book-by-publishedDate";

            homeViewModel.MostPopularBooks = await GetSortedBooks(mostpopularUrl);
            homeViewModel.NewestBooks = await GetSortedBooks(newestUrl);

            homeViewModel.Categories = await GetCategories();

            return View(homeViewModel);
        }



        public async Task<IActionResult> GetByCategory([FromQuery] string categoryName, [FromQuery] int pageIndex=1)
        {
            var bookUrl = BASE_URL + $"Book/get-book-by-category?categoryName={categoryName}&pageIndex={pageIndex}";
            
            var homeViewModel = new HomeViewModel();
            var bookResponse = await _httpClient.Client.GetAsync(bookUrl);
            var DeserilizedBookResponse = JsonConvert.DeserializeObject<ResponseDto<Pagination<GetBookDto>>>(await bookResponse.Content.ReadAsStringAsync());
            
            foreach (var bookDto in DeserilizedBookResponse.Data)
            {
                var count = bookDto.Rate.Count();
                var totalRate = 0;

                if (count != 0)
                {
                    totalRate = bookDto.Rate.Sum(rating => rating.Rate) / count;
                }
                var bookViewModel = new BookViewModel
                {
                    Id = bookDto.Id,
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    PhotoUrl = bookDto.PhotoUrl,
                    Rating = totalRate,
                    Availability = bookDto.Availability
                };
                homeViewModel.Books.Add(bookViewModel);
            };
            
            homeViewModel.Categories = await GetCategories();

            return View("Index", homeViewModel);
        }

        public async Task<IActionResult> Search([FromQuery] SearchViewModel searchViewModel) 
        {
            if(searchViewModel.SearchProperty == null || searchViewModel.SearchTerm == null)
            {
                return RedirectToAction("Index");
            }
          

            var bookUrl = BASE_URL + $"Book/search-for-book?SearchTerm={searchViewModel.SearchTerm}&SearchProperty={searchViewModel.SearchProperty}&PageIndex={searchViewModel.PageIndex}";
            
            var homeViewModel = new HomeViewModel();
            var bookResponse = await _httpClient.Client.GetAsync(bookUrl);
            var DeserilizedBookResponse = JsonConvert.DeserializeObject<ResponseDto<Pagination<GetBookDto>>>(await bookResponse.Content.ReadAsStringAsync());

            foreach (var bookDto in DeserilizedBookResponse.Data)
            {
                var count = bookDto.Rate.Count();
                var totalRate = 0;

                if (count != 0)
                {
                    totalRate = bookDto.Rate.Sum(rating => rating.Rate) / count;
                }
                var bookViewModel = new BookViewModel
                {
                    Id = bookDto.Id,
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    PhotoUrl = bookDto.PhotoUrl,
                    Rating = totalRate,
                    Availability = bookDto.Availability
                };
                homeViewModel.Books.Add(bookViewModel);
            };

            homeViewModel.Categories = await GetCategories();

            return View("Index", homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<CategoryViewModel>> GetCategories()
        {
            var categoryUrl = BASE_URL + "category";
            var categoryResponse = await _httpClient.Client.GetAsync(categoryUrl);
            var DeserilizedCategoryResponse = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<GetCategoryDto>>>(await categoryResponse.Content.ReadAsStringAsync());

            var categories = new List<CategoryViewModel>();
            
            foreach (var categoryDto in DeserilizedCategoryResponse.Data)
            {
                var category = new CategoryViewModel
                {
                    Id = categoryDto.Id,
                    Name = categoryDto.Name
                };

                categories.Add(category);
            };

            return categories;
        }

        private async Task<List<BookViewModel>> GetSortedBooks(string sortUrl)
        {
            var bookResponse = await _httpClient.Client.GetAsync(BASE_URL + sortUrl);

            var deserializedBookResponseObject = JsonConvert.DeserializeObject<ResponseDto<Pagination<GetBookDto>>>(await bookResponse.Content.ReadAsStringAsync());
            var deserializedBookResponse = deserializedBookResponseObject.Data;
            var model = new List<BookViewModel>();

            foreach (var bookDto in deserializedBookResponse)
            {
                var count = bookDto.Rate.Count();
                var totalRate = 0;

                if (count != 0)
                {
                    totalRate = bookDto.Rate.Sum(rating => rating.Rate) / count;
                }

                var book = new BookViewModel
                {
                    Id = bookDto.Id,
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Availability = bookDto.Availability,
                    PhotoUrl = bookDto.PhotoUrl,
                    Rating = totalRate
                };

                model.Add(book);
            }

            return model;
        }
    }
}
