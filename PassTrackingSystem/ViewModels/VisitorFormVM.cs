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
        public int ValidTemporaryPasses{ get => Visitor?.TemporaryPasses?.Where(v=> v?.ValitUntil > DateTime.Now).Count() ?? 0; }
        public string MaximumUntilTemporaryPasses { get => CalculateCurentDate(Visitor?.TemporaryPasses?.Max(v => v?.ValitUntil)); }
        public int AllSinglePasses { get => Visitor?.SinglePasses?.Count ?? 0; }
        public int ValidSinglePasses { get => Visitor?.SinglePasses?.Where(v => v?.ValitUntil > DateTime.Now).Count() ?? 0; }
        public string MaximumUntilSinglePasses { get => CalculateFullCurentDate(Visitor?.SinglePasses?.Max(v => v?.ValitUntil)); }
        public int AllShootingPermissions { get => Visitor?.ShootingPermissions?.Count ?? 0; }
        public int ValidShootingPermissions { get => Visitor?.ShootingPermissions?.Where(v => v?.ValitUntil > DateTime.Now).Count() ?? 0; }
        public string MaximumUntilShootingPermissions { get => CalculateCurentDate(Visitor?.ShootingPermissions?.Max(v => v?.ValitUntil)); }
        public int AllCarPasses { get => Visitor?.CarPasses?.Count ?? 0; }
        public int ValidCarPasses { get => Visitor?.CarPasses?.Where(v => v?.ValitUntil > DateTime.Now).Count() ?? 0; }
        public string MaximumUntilCarPasses { get => CalculateCurentDate(Visitor?.CarPasses?.Max(v => v?.ValitUntil)); }
        public bool ShowAdvancedFeatures { get; set; }


        private string CalculateCurentDate(DateTime? date)
        {
            if (date != null)
            {
                return date >= DateTime.Now ? date.Value.ToShortDateString() : "-";
            }
            else return "-";
        }

        private string CalculateFullCurentDate(DateTime? date)
        {
            if (date != null)
            {
                return date >= DateTime.Now ? date.Value.ToString() : "-";
            }
            else return "-";
        }
    }
  
}
