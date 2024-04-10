using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.Common
{
    public class PagedList<T> 
    {

        private PagedList(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public List<T> Items { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public static PagedList<T> Create(IQueryable<T> query, SearchBase search)
        {
            if (search.PageSize == -1)
            {
                var items = query.ToList();
                return new PagedList<T>(items, search.PageIndex, search.PageSize, items.Count);
            }
            else
            {
                var totalCount = query.Count();
                var items = query.Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize).ToList();
                return new PagedList<T>(items, search.PageIndex, search.PageSize, totalCount);

            }
          
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, SearchBase search)
        {
            if (search.PageSize == -1)
            {
                var items = await query.ToListAsync();
                return new PagedList<T>(items, search.PageIndex, search.PageSize, items.Count);
            }
            else
            {
                var totalCount = query.Count();
                var pageSize = search.PageSize == -1 ? totalCount : search.PageSize;
                var items = await query.Skip((search.PageIndex - 1) * search.PageSize).Take(pageSize).ToListAsync();
                return new PagedList<T>(items, search.PageIndex, search.PageSize, totalCount);

            }
           
        }
    }
}
