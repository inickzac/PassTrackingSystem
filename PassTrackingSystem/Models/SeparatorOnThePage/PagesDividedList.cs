using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models.SeparatorOnThePage
{
    public class PagesDividedList<T> where T:class
    {
        public PageDividorInfo PageDividorInfo = new PageDividorInfo();

        public List<T> Items { get; set; } = new List<T>();
        
        public PagesDividedList(IQueryable<T> query, int currentPage, int pageSize)
        {
            PageDividorInfo.CurrentPage = currentPage;
            PageDividorInfo.PageSize = pageSize;
            PageDividorInfo.TotalPages = query.Count() / PageDividorInfo.PageSize;
            if (PageDividorInfo.TotalPages < currentPage && PageDividorInfo.TotalPages > 0) 
            { 
                PageDividorInfo.CurrentPage = PageDividorInfo.TotalPages; 
            }
            Items.AddRange(query
                .Skip((PageDividorInfo.CurrentPage - 1) * PageDividorInfo.PageSize)
                .Take(PageDividorInfo.PageSize));
        }
    }
}
