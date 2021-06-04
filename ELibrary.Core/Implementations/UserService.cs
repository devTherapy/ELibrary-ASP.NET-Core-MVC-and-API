using AutoMapper;
using ELibrary.Core.Abstractions;
using ELibrary.Data.Repositories.Implementations;
using ELibrary.Dtos;
using ELibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }
        public async Task<ResponseDto<bool>> DeleteUserAsync(string userId)
        {
            var response = new ResponseDto<bool>();
            var user = await _userManager.FindByIdAsync(userId);
            //var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Successful Delete";
                    response.StatusCode = 200;
                    return response;
                }
                else
                {
                    response.Data = false;
                    response.Success = false;
                    response.Message = "Internal Server Error";
                    response.StatusCode = 500;
                    return response;
                }
            }
            response.Data = false;
            response.Success = false;
            response.Message = "Bad Request";
            response.StatusCode = 400;
            return response;
        }

        public async Task<ResponseDto<GetUserDto>> GetUserByIdAsync(string userId)
        {
            var response = new ResponseDto<GetUserDto>();
            if (string.IsNullOrEmpty(userId))
            {

                response.Data = null;
                response.Message = "Bad Request";
                response.StatusCode = 400;
                response.Success = false;
                return response;
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Data = null;
                response.Message = "Not Found";
                response.StatusCode = 404;
                response.Success = false;
                return response;
            }
            var userData = _mapper.Map<GetUserDto>(user);
            response.Data = userData;
            response.Message = "Successful Request";
            response.StatusCode = 200;
            response.Success = true;
            return response;
        }

        public async Task<ResponseDto<Pagination<GetUserDto>>> GetUsersAsync(int pageIndex)
        {
            
            var mappedUsers = _userManager.Users.Select(user => _mapper.Map<GetUserDto>(user));
            var pageSize = int.Parse(_config.GetSection("PageSize:Default").Value);

            var paginatedUsers = await Pagination<GetUserDto>.CreateAsync(mappedUsers, pageIndex, pageSize);

            var response = new ResponseDto<Pagination<GetUserDto>>
            {
                Data = paginatedUsers,
                Success = true,
                StatusCode = 200,
                Message = "Success",
                Prev = paginatedUsers.HasPreviousPage,
                Next = paginatedUsers.HasNextPage
            };

            return response;

        }

        public IEnumerable< UserSearchTermResponseDto> UserSearch(UserBySearchTermDto model)
        {
            var users = new List<UserSearchTermResponseDto>();
            
            if (model.SearchProperty == "Name")
            {
                var userList = _userManager.Users.Where(x => x.FirstName.Contains(model.SearchTerm) || x.LastName.Contains(model.SearchTerm)).ToList();
                foreach (var user in userList)
                {
                    var userResponse = new UserSearchTermResponseDto
                    {
                        Email = user.Email,
                        FirstName=user.FirstName,
                        Id=user.Id,
                        LastName=user.LastName,
                        PhoneNumber=user.PhoneNumber,
                        PhotoUrl=user.PhotoUrl,
                        
                    };
                    users.Add(userResponse);
                }
               
            }
            if (model.SearchProperty == "Email")
            {
                var userList = _userManager.Users.Where(x => x.Email==model.SearchTerm).ToList();
                foreach (var user in userList)
                {
                    var userResponse = new UserSearchTermResponseDto
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        Id = user.Id,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        PhotoUrl = user.PhotoUrl,

                    };
                    users.Add(userResponse);
                }
            }
            return users;
        }
    }
}
