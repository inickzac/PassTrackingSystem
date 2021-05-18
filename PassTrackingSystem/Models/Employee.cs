using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }        
        public string Position { get; set; }
        [Required]
        public string Patronymic { get; set; }
        public Department Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
