using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Data.Repositories.Implementations
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ELibraryDbContext _ctx;
        private readonly DbSet<Review> _entity;

        public ReviewRepository(ELibraryDbContext context) : base(context)
        {
            _ctx = context;
            _entity = _ctx.Set<Review>();
        }



    }
}


