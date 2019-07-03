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

        public DbSet<Sequence> Sequences { get; set; }
    }
}
