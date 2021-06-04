using ELibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.Data.Repositories.Abstractions
{
    public interface IRepository<T> where T : class 
    {
        Task<T> GetById(int id);
        IQueryable<T> GetAll();
        Task<bool> Save(T model);
        Task<bool> Update(T model);
        Task<bool> DeleteById(int id);
    }
}