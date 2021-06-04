using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Repositories.Abstractions
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<Book> GetByCategoryName(string categoryName);
        IQueryable<Book> GetBookByTitle(string bookTitle);
        Task<Book> GetBookByID(int bookId);

        IQueryable<Book> Get();

    }
}
