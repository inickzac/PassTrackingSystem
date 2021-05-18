using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class SinglePass
    {
        public int Id { get; set; }
        [Required]
        public DateTime ValidWith { get; set; }
        [Required]
        public DateTime ValitUntil { get; set; }
        public string PurposeOfIssuance { get; set; }
        public Employee TemporaryPassIssued { get; set; }
        public IList<StationFacility> StationFacilities { get; set; }
        public Visitor Visitor { get; set; }
        public int VisitorId { get; set; }
    }
}
