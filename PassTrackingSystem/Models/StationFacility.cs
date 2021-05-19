using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class StationFacility
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public IList<TemporaryPass> TemporaryPasses { get; set; }
        public IList<SinglePass> SinglePasses { get; set; }
        public IList<ShootingPermission> ShootingPermissions { get; set; }
    }
}
