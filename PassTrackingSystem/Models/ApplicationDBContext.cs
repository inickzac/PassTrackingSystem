using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<IssuingAuthority> IssuingAuthorities { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }
    }
}
