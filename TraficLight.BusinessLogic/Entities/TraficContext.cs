using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraficLight.BusinessLogic.Entities
{
    public class TraficContext : DbContext
    {
        public TraficContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=trafic.db");
        }

        public DbSet<Observation> Observations { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sequence>()
                .HasMany(e => e.Observations)
                .WithOne(e => e.Sequence)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
