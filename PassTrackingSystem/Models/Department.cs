using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
