using ELibrary.Dtos;
using System.Threading.Tasks;

namespace ELibrary.Core.Abstractions
{
    public interface IReviewService
    {
       Task<ResponseDto<ReviewResponseDto>> AddReview(ReviewDto model);
    }
}
