using AutoMapper;
using ELibrary.Dtos;
using ELibrary.Models;
using ELibrary.ViewModels;

namespace ELibrary.Core.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, GetBookDto>();
            CreateMap<AppUser, GetUserDto>();
            CreateMap<GetUserDto, UserViewModel>();
        }
    }
}
