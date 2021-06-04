using ELibrary.Core.Abstractions;
using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Dtos;
using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Core.Implementations
{
   public  class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepo = reviewRepository;
        }
        public async Task<ResponseDto<ReviewResponseDto>> AddReview(ReviewDto model)
        {
            var newReview = new Review
            {
                AppUserId = model.AppUserId,
                Body = model.Body,
                BookId = model.BookId,
                CreatedAt = DateTime.Now,
                Subject = model.Subject,

            };

            var result = await _reviewRepo.Save(newReview);

            if(result == true)
            {

                return new ResponseDto<ReviewResponseDto>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Review succesfully added!",
                    Data = new ReviewResponseDto { ReviewId=newReview.Id}
                };
            }
            return new ResponseDto<ReviewResponseDto>
            {
                StatusCode = 501,
                Success =false,
                Message = "Review was not added!",
                Data = new ReviewResponseDto { }
            };


          



        }
    }
}
