using ELibrary.Data.Repositories.Implementations;
using ELibrary.Dtos;
using System.Threading.Tasks;

namespace ELibrary.Core.Abstractions
{
   public interface IBookService
   {
        public Task<ResponseDto<Pagination<GetBookDto>>> GetByCategory(string CategoryName, int pageSize);

        public Task<ResponseDto<Pagination<GetBookDto>>> SortByDate(int pageIndex = 1);
        public Task<ResponseDto<Pagination<GetBookDto>>> SortByViews(int pageIndex = 1);
    }
}
