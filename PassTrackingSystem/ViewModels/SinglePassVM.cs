using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class SinglePassVM : ITableWithOptionsVM
    {
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
                { "id", "Id" },
                { "Значение",  "Value"},
            };
        public PagesDividedList<SinglePass> SinglePasses { get; set; }
        public SinglePass ProcessingSinglePass { get; set; }
        public int? PurposeVisitorId { get; set; }
        public bool ShowAdvancedFeatures { get; set; }
    }
}
