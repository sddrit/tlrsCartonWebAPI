using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tlrsCartonManager.DAL.Models.MetaData;

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

        public virtual DbSet<MobileDevice> MobileDevices { get; set; }

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

            modelBuilder.Entity<MobileDevice>(entity =>
            {
                entity.HasKey(e => e.DeviceCode)
                    .HasName("PK_Mobile-Devices");

                entity.Property(e => e.DeviceCode).IsUnicode(false);

                entity.Property(e => e.DeviceName).IsUnicode(false);

                entity.Property(e => e.LastSynchedUser).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
