using ELibrary.Core.Abstractions;
using ELibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ELibrary.MVC.Controllers.ApiControllers
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        private readonly IAuthServices _authservices;
     

        public AuthController(IAuthServices authServices)
        {
            _authservices = authServices;
          
        }



        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authservices.RegisterUserAsync(model);
                return Ok(result);
            }
            return BadRequest("not successful!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDetailDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authservices.LoginUserAsync(model);
                return Ok(result);
            }
            return BadRequest("Some properties are not valid");

        }

        [HttpPost("Logout")]
        public IActionResult LogOut()
        {
            var result = _authservices.Logout();
            return Ok(result);

        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authservices.ConfirmEmailAsync(userid, token);

            return Ok(result);
        }
       
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody]ForgotPwdDto model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return NotFound();

            var result = await _authservices.ForgetPasswordAsync(model.Email, Url, Request.Scheme);

            if (result.Success)
                return Ok(result);
            
            return BadRequest(result); 
        }




        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authservices.ResetPasswordAsync(model);

                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
