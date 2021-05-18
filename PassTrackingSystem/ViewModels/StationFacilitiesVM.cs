using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class StationFacilitiesVM :ITableWithOptionsVM
    {
        public Dictionary<StationFacility, bool> AccessPairs { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
          new Dictionary<string, string> {
                { "id", "Id" },
                { "Значение",  "Value"},
          };
        public CommonListQuery Options { get; set; }
    }    
}
