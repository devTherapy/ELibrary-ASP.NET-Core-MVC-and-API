using ELibrary.Core.Abstractions;
using ELibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ELibrary.MVC.Controllers.ApiControllers
{
 
    [AllowAnonymous]
    public class UserController : BaseApiController
    {
        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var responseData = await _userService.GetUserByIdAsync(userId);
            if (responseData.StatusCode == 400)
                return BadRequest(responseData);
            if (responseData.StatusCode == 404)
                return NotFound(responseData);
            return Ok(responseData);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var responseData = await _userService.DeleteUserAsync(userId);
            if (responseData.StatusCode == 400)
                return BadRequest(responseData);
            if (responseData.StatusCode == 404)
                return NotFound(responseData);
            return Ok(responseData);
        }
        [HttpGet("all-user")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageIndex)
        {
            var paginatedUsers = await _userService.GetUsersAsync(pageIndex);
            return Ok(paginatedUsers);
        }

        [HttpGet("SearchTerm")]
        public IActionResult GetUsers([FromForm] UserBySearchTermDto model )
        {
            var users = _userService.UserSearch(model);
            if (users== null)
            {
                NotFound("User not found!");
            }
            return Ok(users);
        }


    }
}
