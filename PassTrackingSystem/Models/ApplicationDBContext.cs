using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<IssuingAuthority> IssuingAuthorities { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<StationFacility> StationFacilities { get; set; }
        public DbSet<TemporaryPass>  TemporaryPasses { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPass> CarPasses { get; set; }
        public DbSet<SinglePass> SinglePasses { get; set; }
        public DbSet<ShootingPermission> ShootingPermissions  { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        
        }     
    }
}
