using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    public partial class tlrmCartonContextUpdated : DbContext
    {
        public tlrmCartonContextUpdated()
        {
        }

        public tlrmCartonContextUpdated(DbContextOptions<tlrmCartonContextUpdated> options)
            : base(options)
        {
        }

        public virtual DbSet<TaxEffectiveDate> TaxEffectiveDates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=tlrmCartonConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TaxEffectiveDate>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.TaxCode).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
