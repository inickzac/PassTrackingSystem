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
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
            // Database.EnsureDeleted(); 
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Visitor>().HasOne(t => t.Document)
        //             .WithOne(t => t.Visitor)
        //              .HasForeignKey<Document>(t => t.VisitorId);

        //    modelBuilder.Entity<Document>().HasOne(t => t.Visitor)
        //             .WithOne(t => t.Document)
        //             .HasForeignKey<Visitor>(t => t.DocumentId)
        //             .IsRequired();
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
