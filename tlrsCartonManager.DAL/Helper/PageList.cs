using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Helper
{

    public class PageList<T> : List<T>
    {
        public PageList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }

    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    [Serializable]
    public class PagedListSP<T> : List<T>
    {       
        //public PagedListSP(IQueryable<T> source, int pageIndex, int pageSize, bool getOnlyTotalCount = false)
        //{
        //    var total = source.Count();
        //    TotalCount = total;
        //    TotalPages = total / pageSize;

        //    if (total % pageSize > 0)
        //        TotalPages++;

        //    PageSize = pageSize;
        //    PageIndex = pageIndex;
        //    Items = (IList<T>)source;
        //    if (getOnlyTotalCount)
        //        return;
        //    AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        //}
       
        public PagedListSP(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            Items = (IList<T>)source;
            //AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

    
        public PagedListSP(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            Items = (IList<T>)source;
            AddRange(source);
        }       

     
        public int PageIndex { get; }
               
        public int PageSize { get; }
        
        public int TotalCount { get; }
        
        public int TotalPages { get; }
        public bool HasPreviousPage => PageIndex > 0;
       
        public bool HasNextPage => PageIndex + 1 < TotalPages;

        public IList<T> Items { get; set; }

    }
}
