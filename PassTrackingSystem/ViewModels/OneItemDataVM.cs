using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class OneItemDataVM : ITableWithOptionsVM
    {
        public PagesDividedList<IOneValueCommon> OneValues { get; set; }
        public CommonListQuery Options { get; set; }
        public string ExtendMenuHeader { get; set; } 
        public Dictionary<string, string> HeadNames { get; set; } = 
            new Dictionary<string, string> { 
                { "id", "Id" },
                { "Значение",  "Value"},
            };
    }
}
