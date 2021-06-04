using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Data.Repositories.Abstractions;
using ELibrary.Dtos;
using ELibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Data.Repositories.Implementations
{
    public class GenericRepository<T>: IRepository<T> where T: class
    {
        protected readonly ELibraryDbContext _context;

        public GenericRepository(ELibraryDbContext context)
        {
            _context = context;
        }
        
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<bool> Save(T model)
        {
            await _context.Set<T>().AddAsync(model);
            return await _context.SaveChangesAsync() >= 1;
            }

        public async Task<bool> Update(T model)
        {
            _context.Set<T>().Update(model);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeleteById(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(model);
            return await _context.SaveChangesAsync() >= 1;
        }




    }
}