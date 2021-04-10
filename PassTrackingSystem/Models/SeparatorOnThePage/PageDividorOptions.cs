using System;
using System.Reflection;


namespace PassTrackingSystem.Models.SeparatorOnThePage
{
    public class PageDividorOptions
    {
        public PageDividorOptions(string orderByPropertyName)
        {
            OrderPropertyName = orderByPropertyName;
        }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public string OrderPropertyName { get; set; }
        public bool IsOrderByDescending { get; set; } = false;
    }
}
