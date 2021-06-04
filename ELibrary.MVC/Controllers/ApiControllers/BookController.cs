using AutoMapper;
using ELibrary.Common.Helpers;
using ELibrary.Core.Abstractions;
using ELibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ELibrary.MVC.Controllers.ApiControllers
{
    [AllowAnonymous]
    public class BookController : BaseApiController
    {
        private readonly IBookServices _bookServices;
        private readonly IBookService _bookService;
        private readonly IRateService _rateService;


        public BookController(IBookServices bookServices, IBookService bookService, IRateService rateService)
        {
            _bookServices = bookServices;
            _bookService = bookService;
            _rateService = rateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex=1)
        {
            var response = await _bookServices.GetAll(pageIndex);
            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var response = await _bookServices.DeleteById(id);
            if (response.Success)
                return Ok(response);

            return Ok(response);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateBooKPhoto([FromForm] AddPhotoDto photo)
        {
            var response = await _bookServices.UpdatePhotoBook(photo);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto book)
        {
            if (book == null)
                return NotFound();

            var result = await _bookServices.AddBook(book);

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto book)
        {
            if (book == null)
            {

            }
                //return NotFound();

            var result = await _bookServices.UpdateBook(book);

            return Ok(result);
        }

        [HttpGet("get-by-title")]
        public async Task<IActionResult> GetBookByTitle([FromQuery] BookByTitleResourceParameters bookResource)
        {

            if (bookResource == null)
                return NotFound();

            var result = await _bookServices.GetBookByTitle(bookResource);

            return Ok(result);
        }


        [Authorize(AuthenticationSchemes ="Bearer")]
        [HttpGet("{bookId}", Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(string bookId)
        {
            var isNumeric = int.TryParse(bookId, out int n);

            if (!isNumeric)
                return NotFound();

            var result = await _bookServices.GetBookById(bookId);

            return Ok(result);
        }




        [HttpGet("get-book-by-category")]
        public async Task<IActionResult> GetBookByCategory([FromQuery] GetBookByCategoryDto getBook)
        {
            var result = await _bookService.GetByCategory(getBook.categoryName, getBook.pageIndex);
            if (result != null)
            {
                return Ok(result); // 200
            }
            return NotFound(result); // 404
        }

        [HttpPost("rate-a-book")]
        public async Task<IActionResult> RateABook([FromBody] RateABookDto rateABook)
        {
            var result = await _rateService.RateBook(rateABook.BookId, rateABook.RatingValue, rateABook.UserId);
            if (result != null)
            {
                return Ok(result); // 200
            }
            return NotFound(); // 404
        }

        [HttpGet("search-for-book")]
        public async Task<IActionResult> SearchForBook([FromQuery] SearchBookDto getBook)
        {
            var result = await _bookServices.GetBookBySearchTerm(getBook.SearchTerm, getBook.SearchProperty, getBook.PageIndex);
            if (result != null)
            {
                return Ok(result); // 200
            }
            return NotFound(result); // 404
        }


        [HttpGet("sort-book-by-publishedDate")]
        public async Task<IActionResult> SortBookByPublishedDate([FromQuery] int pageIndex)
        {
            var result = await _bookService.SortByDate(pageIndex);
            if (result != null)
            {
                return Ok(result); // 200
            }
            return NotFound(result); // 404 
        }

        [HttpGet("sort-book-by-view")]
        public async Task<IActionResult> SortBookByView([FromQuery] int pageIndex)
        {
            var result = await _bookService.SortByViews(pageIndex);
            if (result != null)
            {
                return Ok(result); // 200
            }
            return NotFound(result); // 404
        }



        [HttpGet("{bookId:int}/rate")]
        public async Task<IActionResult> GetBookRate(int bookId)
        {
            var response = await _rateService.GetBookRate(bookId);
            
            return Ok(response);
        }
    }
}