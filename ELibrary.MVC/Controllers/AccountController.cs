using ELibrary.Common.Helpers;
using ELibrary.Dtos;
using ELibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ELibrary.MVC.Controllers
{
    public class AccountController : Controller
    {
        private const string BASE_URL = "https://localhost:44326/";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            var userDto = new LoginDetailDto();

            if (ModelState.IsValid)
            {
                userDto.Email = model.Email;
                userDto.Password = model.Password;
            }
            else
            {
                return View(model);
            }
            var url = BASE_URL + "api/auth/login";
            var client = new ApiHttpClient();
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(userDto)
            };

            var response = await client.Client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ResponseDto<LoginResponseDto>>(content);

            if (responseObject.StatusCode == 200 && !string.IsNullOrEmpty(responseObject.Data.Token))
            {
                HttpContext.Session.SetString("Token", responseObject.Data.Token);
                HttpContext.Session.SetString("UserId", responseObject.Data.UserId);
                HttpContext.Session.SetString("Role", responseObject.Data.Role);
                HttpContext.Session.SetString("Name", responseObject.Data.Name);
            }
            else
            {
                ModelState.AddModelError("LoginError" , "Incorrect Username and Password");
                return View(model);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {

                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var registerDto = new RegistrationDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    Email = model.Email,
                };
                var url = BASE_URL + "api/auth/register";
                var client = new ApiHttpClient();
                var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = JsonContent.Create(registerDto)
                };

                var response = await client.Client.SendAsync(postRequest);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<RegisterViewModel>(content);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View(model);
            }


        }


    }
}
