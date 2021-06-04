using ELibrary.Data.Repositories.Implementations;
using ELibrary.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELibrary.Core.Abstractions
{
    public interface IUserService
    {
        Task<ResponseDto<GetUserDto>> GetUserByIdAsync(string userId);
        Task<ResponseDto<Pagination<GetUserDto>>> GetUsersAsync(int pageIndex);
        Task<ResponseDto<bool>> DeleteUserAsync(string userId);
        IEnumerable<UserSearchTermResponseDto> UserSearch(UserBySearchTermDto model);
    }
}
