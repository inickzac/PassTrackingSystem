using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public interface ITableWithOptionsVM
    {
        Dictionary<string, string> HeadNames { get; set; }
        CommonListQuery Options { get; set; }
    }
}
