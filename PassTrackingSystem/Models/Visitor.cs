using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Patronymic { get; set; }
        public string PlaceOfWork { get; set; }
        public string Position { get; set; }
        public Document Document { get; set; }
        public IList<TemporaryPass> TemporaryPasses { get; set; }
        public IList<SinglePass> SinglePasses { get; set; }
        public IList<ShootingPermission> ShootingPermissions { get; set; }
        public IList<CarPass> CarPasses  { get; set; }
    }
}
