using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class CommonListQuery
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
