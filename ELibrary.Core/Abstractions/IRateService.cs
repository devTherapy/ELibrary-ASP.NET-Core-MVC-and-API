using ELibrary.Dtos;
using System.Threading.Tasks;

namespace ELibrary.Core.Abstractions
{
   public interface IRateService
    {
        Task<ResponseDto<bool>> RateBook(int bookId, int ratingValue, string userId);
        Task<ResponseDto<int>> GetBookRate(int bookId);

    }
}
