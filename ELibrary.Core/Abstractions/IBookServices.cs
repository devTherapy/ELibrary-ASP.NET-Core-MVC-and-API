using System.Threading.Tasks;
using ELibrary.Data.Repositories.Implementations;
using ELibrary.Dtos;
using ELibrary.Models;

namespace ELibrary.Core.Abstractions
{
    public interface IBookServices
    {
        Task<ResponseDto<Pagination<GetBookDto>>> GetAll(int pageIndex);
        Task<ResponseDto<bool>> DeleteById(int bookId);
        Task<ResponseDto<AddBookResponseDto>> AddBook(AddBookDto model);
        Task<ResponseDto<UpdateBookResponseDto>> UpdateBook(UpdateBookDto model);
        Task<ResponseDto<Pagination<GetBookDto>>> GetBookByTitle(BookByTitleResourceParameters bookResource);
        Task<ResponseDto<Book>> GetBookById(string Id);
        public Task<ResponseDto<GetBookDto>> UpdatePhotoBook(AddPhotoDto photo);
        //public Task<ResponseDto<Pagination<GetBookDto>>> GetBookByCategory(string categoryName, int pageIndex);
        public Task<ResponseDto<Pagination<GetBookDto>>> GetBookBySearchTerm(string searchTerm, string searchproperty, int pageNumber);


    }
}
