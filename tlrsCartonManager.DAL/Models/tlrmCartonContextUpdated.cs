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

        public virtual DbSet<MenuModel> MenuModels { get; set; }
        public virtual DbSet<MenuModelOption> MenuModelOptions { get; set; }
        public virtual DbSet<MenuModelOptionsUserRole> MenuModelOptionsUserRoles { get; set; }
        public virtual DbSet<MenuModelUserRole> MenuModelUserRoles { get; set; }
        public virtual DbSet<MenuRightFormName> MenuRightFormNames { get; set; }

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

            modelBuilder.Entity<MenuModel>(entity =>
            {
                entity.Property(e => e.ModelCode).ValueGeneratedNever();
            });

            modelBuilder.Entity<MenuModelOption>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.FormRightId });

                entity.HasOne(d => d.FormRight)
                    .WithMany(p => p.MenuModelOptions)
                    .HasForeignKey(d => d.FormRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuModelOptions_MenuRightFormNames");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.MenuModelOptions)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuModelOptions_MenuModelOptions");
            });

            modelBuilder.Entity<MenuModelOptionsUserRole>(entity =>
            {
                entity.Property(e => e.TrackingId).ValueGeneratedNever();

                entity.HasOne(d => d.FormRight)
                    .WithMany(p => p.MenuModelOptionsUserRoles)
                    .HasForeignKey(d => d.FormRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuModelOptionsUserRole_MenuRightFormNames");
            });

            modelBuilder.Entity<MenuModelUserRole>(entity =>
            {
                entity.HasOne(d => d.Model)
                    .WithMany(p => p.MenuModelUserRoles)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuModelUserRole_MenuModels");
            });

            modelBuilder.Entity<MenuRightFormName>(entity =>
            {
                entity.Property(e => e.FormRightId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
