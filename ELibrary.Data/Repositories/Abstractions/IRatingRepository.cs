using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Repositories.Abstractions
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<Rating> GetByBookIdAndUserIdAsync(int bookId, string userId);
    }
}
