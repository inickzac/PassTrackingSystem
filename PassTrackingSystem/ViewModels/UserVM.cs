using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class UserVM : ITableWithOptionsVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

        public Employee  Employee { get; set; }
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
                { "id", "Id" },
                { "Значение",  "Value"},
            };
        public Dictionary<AppUser, Employee> userEmployeePairs { get; set; }

    }
}
