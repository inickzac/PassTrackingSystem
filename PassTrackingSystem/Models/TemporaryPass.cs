using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class TemporaryPass
    {
        public int Id { get; set; }
        public DateTime ValidWith { get; set; }
        public DateTime ValitUntil { get; set; }
        public string PurposeOfIssuance { get; set; }
        public Employee TemporaryPassIssued { get; set; }
        public IList<StationFacility> StationFacilities { get; set; }
    }
}
