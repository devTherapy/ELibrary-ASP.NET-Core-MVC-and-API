using ELibrary.Core.Abstractions;
using ELibrary.Dtos;
using ELibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Core.Implementations
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailServices _mailService;
        private readonly string path = "../ELibrary.MVC/Controllers/ApiControllers";



        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly ICloudinaryServices cloudinary;
        private readonly IEmailServices emailServices;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IConfiguration configuration;


        public AuthServices(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            ICloudinaryServices cloudinary, IEmailServices emailServices,
            IJwtTokenGenerator jwtTokenGenerator, IConfiguration configuration, IEmailServices mailService)
        {
            _userManager = userManager;
            _mailService = mailService;

            this.signInManager = signInManager;
            //this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.emailServices = emailServices;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.configuration = configuration;

        }


        public async Task<ResponseDto<RegisterResponseDto>> RegisterUserAsync(RegistrationDto model)
        {


            if (model.Password != model.ConfirmPassword)
            {
                return new ResponseDto<RegisterResponseDto>
                {
                    StatusCode = 401,
                    Success = false,
                    Message = "Password Mismatch",
                    Data = new RegisterResponseDto { }
                };
            }

            var appUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,

            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            var role = await _userManager.AddToRoleAsync(appUser, "Regular");

            if (result.Succeeded && role.Succeeded)
            {
                var userFromDb = await _userManager.FindByEmailAsync(model.Email);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
                var encodedEmailToken = Encoding.UTF8.GetBytes(token);
                var vaidEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                string url = $"{configuration["AppUrl"]}/api/Auth/ConfirmEmail?userid={userFromDb.Id}&token={vaidEmailToken}";
                var email = emailServices.SendEmail(new Email
                {
                    To = model.Email,
                    Body = "<h1>Follow the instructions to confirm your password</h1>" +
                $"<p>To confirm your password <a href='{url}'>Click here</a></p>"
                });
                if (model.PhotoFile != null)
                {
                    var uploadResult = cloudinary.UploadImage(model.PhotoFile);

                    if (uploadResult == null)
                    {
                        throw new NullReferenceException("Unable to upload Picture");
                    }
                    appUser.PhotoUrl = uploadResult.Result.Url.ToString();
                    await _userManager.UpdateAsync(appUser);
                }
                return new ResponseDto<RegisterResponseDto>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "You have successfully registered and have been sent a confirmation link in  your email, Click on the link to confirm your Email",
                    Data = new RegisterResponseDto { UserId = userFromDb.Id }
                };
            }
            string errors = "";
            foreach (var error in result.Errors)
            {
                errors += error.ToString() + "\n";
            }
            return new ResponseDto<RegisterResponseDto>
            {
                StatusCode = 401,
                Success = false,
                Data = new RegisterResponseDto { },
                Message = errors
            };
        }


        public async Task<ResponseDto<LoginResponseDto>> LoginUserAsync(LoginDetailDto model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null && await _userManager.CheckPasswordAsync(appUser, model.Password) == false)
            {
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 401,
                    Success = false,
                    Message = "Invalid Login Credentials!",
                    Data = new LoginResponseDto { }
                };
            }

            var userRoles = await _userManager.GetRolesAsync(appUser);
            var token = jwtTokenGenerator.GenerateToken(appUser.UserName, appUser.Id, appUser.Email, configuration, userRoles.ToArray());
            if (!string.IsNullOrEmpty(token))
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RemeberMe, true);
                if (result.Succeeded)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 200,
                        Success = true,
                        Message = "Login Successful!",
                        Data = new LoginResponseDto { Email = appUser.Email, Token = token, UserId = appUser.Id, Role = userRoles[0], Name = appUser.FirstName }
                    };
                }

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 401,
                    Success = false,
                    Data = new LoginResponseDto { },
                    Message = "Was not Able to Login!"
                };
            }
            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 401,
                Success = false,
                Data = new LoginResponseDto { },
                Message = "Could not Generate Token"
            };

        }


        public ResponseDto<LogOutDto> Logout()
        {
            signInManager.SignOutAsync();
            return new ResponseDto<LogOutDto>
            {
                StatusCode = 200,
                Success = true,
                Message = "User has successfully Logged Out!",
                Data = new LogOutDto { IsLoggedOut = true }

            };

        }

        public async Task<ConfirmEmailResponseDto> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ConfirmEmailResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new ConfirmEmailResponseDto
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new ConfirmEmailResponseDto
            {
                IsSuccess = false,
                Message = "Email did not confirmed",

            };
        }

        public async Task<ResponseDto<string>> ForgetPasswordAsync(string email, IUrlHelper url, string requestScheme)
        {
            { 
            ResponseDto<string> response = new ResponseDto<string>();


                if (string.IsNullOrEmpty(email))
                {
                    response.Message = "Please provide an email";
                    return response;
                }


            var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    response.Message = "email does not exist";
                    return response;
                }


            var token = await EmailForgotPasswordToken(user);


                var passwordResetLink = url.Action("ResetPassword", "Auth", new { email, token }, requestScheme);


                var message = new Email
                {
                    Subject = "ResetPassword",
                    Body = "Reset Password" +
                    "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{passwordResetLink}'>Click here</a></p>",
                    To = email
                };
                var mailSent =  _mailService.SendEmail(message);


                if (mailSent)
                {
                    response.Success = true;
                    response.Message = "Link sent to the email successfully";
                    return response;
                }


            response.Message = "Mail failed to send";
                return response;
            }

        }

        private async Task<string> EmailForgotPasswordToken(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            return validToken;
        }

  

        public async Task<ResponseDto<string>> ResetPasswordAsync(ResetPasswordDto model)
        {
            ResponseDto<string> response = new ResponseDto<string>();
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            { 
                response.Message = "No user associated with email";
                return response;
            }


            if (model.Password != model.ConfirmPassword)
            {
                response.Message = "Password doesn't match its confirmation";
                return response;
            }


            var decodedToken = WebEncoders.Base64UrlDecode(model.token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);


            if (result.Succeeded)
            {
                response.Success = true;
                response.Message = "Password has been reset successfully!";
                return response;
            }


            response.Message = "Something went wrong";
            return response;

        }
    }
}
