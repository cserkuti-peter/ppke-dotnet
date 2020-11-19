using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList(List<T> items, int totalcount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalcount / (double)pageSize);

            this.AddRange(items);
        }

        public PagedList(List<T> items, int totalPages, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;

            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreatePagedListAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var totalcount = await source.CountAsync();
            var items = await source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedList<T>(items, totalcount, pageIndex, pageSize);
        }


    }
}
