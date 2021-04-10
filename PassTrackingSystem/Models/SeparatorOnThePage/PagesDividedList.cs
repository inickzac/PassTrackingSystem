using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models.SeparatorOnThePage
{
    public class PagesDividedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public PagesDividedList(IQueryable<T> query, PageDividorOptions options = null)
        {
            if (options == null) options = new PageDividorOptions();
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            TotalPages = query.Count() / PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
        }
    }
}
