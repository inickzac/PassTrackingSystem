using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels.OneItemComboBoxVM
{
    public class OneItemComboBoxVMInner
    {
       public List<IOneValueCommon> OptionsList { get; set; }
       public  int SelectedId { get; set; }
    }
}
