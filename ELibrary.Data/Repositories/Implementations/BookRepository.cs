using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Dtos;
using ELibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.Data.Repositories.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {

        public BookRepository(ELibraryDbContext context) : base(context)
        {

        }

        public IQueryable<Book> Get()
        {
            return _context.Books.Include(book => book.Rate);
        }

        public IQueryable<Book> GetBookByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return null;
            }

            title = title.Trim(); ;

            var bookCollection = _context.Books.Where(book => book.Title.Contains(title));

            return bookCollection;
        }

        public async Task<Book> GetBookByID(int bookId)
        {
            var booksReturned = await _context.Books
                .Where(e => e.Id == bookId)
                .Include(e => e.Category)
                .Include(e => e.Reviews)
                .Include(e => e.Rate).FirstOrDefaultAsync();
            return booksReturned;
        }

        public IQueryable<Book> GetByCategoryName(string CategoryName)
        {
            var booksReturned = _context.Books
                .Where(e => e.Category.Name == CategoryName)
                .Include(e => e.Category)
                .Include(e => e.Reviews)
                .Include(e => e.Rate);
            return booksReturned;
        }
    }
}