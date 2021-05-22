using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public enum Role
    {
        Administrator , Moderator, User
    }
    public class UserVM : ITableWithOptionsVM
    {
        [Required]
        public AppUser AppUser { get; set; }
        public string Password { get; set; }
        public Employee  Employee { get; set; }
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
                { "id", "Id" },
                { "Значение",  "Value"},
            };
        public Dictionary<AppUser, Employee> userEmployeePairs { get; set; }
        public Role UserRole { get; set; }
        public PageDividorInfo PageDividorInfo { get; set; }
    }
}
