using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.Data.Repositories.Implementations
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ELibraryDbContext context) : base(context)
        {
        }

        public async Task<Rating> GetByBookIdAndUserIdAsync(int bookId, string userId)
        {
            var rating = await _context.Ratings.Where(r => r.BookId == bookId && r.AppUserId == userId).FirstOrDefaultAsync();
            return rating;
        }
    }
}