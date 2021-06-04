using ELibrary.Core.Abstractions;
using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Dtos;
using ELibrary.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Core.Implementations
{
    public class RateService : IRateService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<AppUser> _userManager;

        public RateService(IRatingRepository ratingRepository, IBookRepository bookRepository, UserManager<AppUser> userManager)
        {
            _ratingRepository = ratingRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        public async Task<ResponseDto<int>> GetBookRate(int bookId)
        {
            var response = new ResponseDto<int>();

            var bookRate = await _ratingRepository.GetAll().Where(rate => rate.BookId == bookId).SumAsync(rate => rate.Rate);
            
            response.Data = bookRate;
            response.Success = true;
            response.StatusCode = 200;
            
            return response;
        }

        public async Task<ResponseDto<bool>> RateBook(int bookId, int ratingValue, string userId)
        {
            var response = new ResponseDto<bool>
            {
                Success = false,
                Message = "Number out of range"
                
            };

            if (ratingValue >= 0 && ratingValue <= 5)
            {
                var book = await _bookRepository.GetById(bookId);
                var user = await _userManager.FindByIdAsync(userId);

                if(book == null || user == null)
                {
                    response.Message = "Book or User does not exist";
                    return response;
                }

                var rate = await _ratingRepository.GetByBookIdAndUserIdAsync(bookId, userId);
                if(rate == null)
                {
                    rate = new Rating
                    {
                        Book = book,
                        AppUser = user,
                        AppUserId = userId,
                        Rate = ratingValue
                    };
                }
                else
                {
                    rate.Rate = ratingValue;
                }
               
                var result = await _ratingRepository.Update(rate);
                if (result == true)
                {
                    response.Success = true;
                    response.StatusCode = 200;
                    response.Message = "Success";
                    response.Data = true;
                    return response;
                }
            }
            return response;
        }
    }
}
