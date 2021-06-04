using AutoMapper;
using ELibrary.Dtos;
using ELibrary.Models;
using ELibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Core.Automapper
{
    class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<AddBookDto, Book>();
            CreateMap<AddBookDto, Book>();
            CreateMap<Book, AddBookResponseDto>();
            CreateMap<UpdateBookResponseDto, Book>(); 
            CreateMap<Book, BookByTitleResourceParameters>();
            CreateMap<Book, GetBookDto>();

            CreateMap<GetBookDto, BookDetailViewModel>();
        }
    }
}
