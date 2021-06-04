using AutoMapper;
using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Dtos;
using ELibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ELibrary.MVC.Controllers.ApiControllers
{
    [AllowAnonymous]
    public class CategoryController : BaseApiController
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll().Select(cat => _mapper.Map<GetCategoryDto>(cat));
            var response = new ResponseDto<IEnumerable<GetCategoryDto>> 
            { 
                Data = categories,
                Success = true,
                StatusCode = 200
            };

            return Ok(response);
        }
    }
}
