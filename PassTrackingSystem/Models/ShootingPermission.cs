using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class ShootingPermission
    {
        public int Id { get; set; }
        public DateTime ValidWith { get; set; }
        [Required]
        public DateTime ValitUntil { get; set; }
        public string ShootingPurpose { get; set; }
        public string CameraType { get; set; }
        public IList<StationFacility> StationFacilities { get; set; }
        public Visitor Visitor { get; set; }
        public int VisitorId { get; set; }
    }
}
