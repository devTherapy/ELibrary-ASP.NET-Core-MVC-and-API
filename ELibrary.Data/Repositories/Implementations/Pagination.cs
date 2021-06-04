using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Data.Repositories.Implementations
{
    public class Pagination<T>: List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public Pagination(IEnumerable<T> items, int count, int pageIndex=1, int pageSize=15)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            
            AddRange(items);
        }

        public Pagination()
        {
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => PageIndex < TotalPages;

        public async static Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(items, count, pageIndex, pageSize);
        }
    }
}