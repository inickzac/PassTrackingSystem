using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string CarLicensePlate { get; set; }
        public string CarBrand { get; set; }
        public int CarPassId { get; set; }
        public CarPass CarPass { get; set; }
    }
}
