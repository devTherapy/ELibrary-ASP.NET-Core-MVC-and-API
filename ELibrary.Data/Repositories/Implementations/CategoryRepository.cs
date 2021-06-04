using ELibrary.Models;

namespace ELibrary.Data.Repositories.Implementations
{
    public class CategoryRepository: GenericRepository<Book>
    {
        public CategoryRepository(ELibraryDbContext context): base(context)
        { }
    }
}