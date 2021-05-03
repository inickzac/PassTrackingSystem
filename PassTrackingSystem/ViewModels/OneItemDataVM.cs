using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class OneItemDataVM
    {
        public PagesDividedList<IOneValueCommon> OneValues { get; set; }
        public CommonListQuery Options { get; set; }
    }
}
