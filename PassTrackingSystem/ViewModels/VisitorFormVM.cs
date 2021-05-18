using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class VisitorFormVM
    {   
        public Visitor Visitor { get; set; }
        public int AllTemporaryPasses { get => Visitor?.TemporaryPasses?.Count ?? 0; }
        public int ValidTemporaryPasses{ get => Visitor?.TemporaryPasses?.Where(v=> v.ValitUntil > DateTime.Now).Count() ?? 0; }
        public string MaximumUntilTemporaryPasses { get => Visitor?.TemporaryPasses?.Max(v=> v.ValitUntil).ToShortDateString() ?? "0"; }

    }
}
