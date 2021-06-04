using ELibrary.Core.Abstractions;
using ELibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.MVC.Controllers.ApiControllers
{
    [AllowAnonymous]
    public class ReviewController : BaseApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService review)
        {
            _reviewService = review;
        }



        [HttpPost("AddReview")]
        public async Task <ActionResult> AddReview([FromBody] ReviewDto model)
        {         
           var result = await  _reviewService.AddReview(model);
            if (result.Success) return Ok(result);           
                 return BadRequest(result);
        }
    }
}
