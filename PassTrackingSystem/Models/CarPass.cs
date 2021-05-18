using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class CarPass
    {
        public int Id { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ValidWith { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ValitUntil { get; set; }
        public string PurposeOfIssuance { get; set; }
        public Employee TemporaryPassIssued { get; set; }
        public Car Car { get; set; }
        public Visitor Visitor { get; set; }
        public int VisitorId { get; set; }
    }
}
