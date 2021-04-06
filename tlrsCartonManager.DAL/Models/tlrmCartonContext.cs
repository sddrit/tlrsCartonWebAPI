using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    public partial class tlrmCartonContext : DbContext
    {
        public tlrmCartonContext()
        {
        }

        public tlrmCartonContext(DbContextOptions<tlrmCartonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthorizationLevel> AuthorizationLevels { get; set; }
        public virtual DbSet<BillingCycle> BillingCycles { get; set; }
        public virtual DbSet<CalculationType> CalculationTypes { get; set; }
        public virtual DbSet<CartonLocation> CartonLocations { get; set; }
        public virtual DbSet<CartonStorage> CartonStorages { get; set; }
        public virtual DbSet<CartonType> CartonTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAuthorizationListBak> CustomerAuthorizationListBaks { get; set; }
        public virtual DbSet<CustomerAuthorizationListDetail> CustomerAuthorizationListDetails { get; set; }
        public virtual DbSet<CustomerAuthorizationListHeader> CustomerAuthorizationListHeaders { get; set; }
        public virtual DbSet<CustomerAuthorizationListHeaderTemp> CustomerAuthorizationListHeaderTemps { get; set; }
        public virtual DbSet<CutomerInvoiceProfile> CutomerInvoiceProfiles { get; set; }
        public virtual DbSet<DisposalTimeFrame> DisposalTimeFrames { get; set; }
        public virtual DbSet<InvoiceProfile> InvoiceProfiles { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MenuRight> MenuRights { get; set; }
        public virtual DbSet<MenuRightAttachedUser> MenuRightAttachedUsers { get; set; }
        public virtual DbSet<MenuRightForm> MenuRightForms { get; set; }
        public virtual DbSet<MenuRightFormUser> MenuRightFormUsers { get; set; }
        public virtual DbSet<MenuRightUser> MenuRightUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<SlabTypeDetail> SlabTypeDetails { get; set; }
        public virtual DbSet<SlabTypeHeader> SlabTypeHeaders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActivityLogger> UserActivityLoggers { get; set; }
        public virtual DbSet<UserActivityType> UserActivityTypes { get; set; }
        public virtual DbSet<UserLogger> UserLoggers { get; set; }
        public virtual DbSet<UserPassword> UserPasswords { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

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

            modelBuilder.Entity<AuthorizationLevel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<BillingCycle>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<CalculationType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<CartonLocation>(entity =>
            {
                entity.Property(e => e.BarCode).IsUnicode(false);

                entity.Property(e => e.ContainerType).IsUnicode(false);

                entity.Property(e => e.CorrectedBarCode).IsUnicode(false);

                entity.Property(e => e.LocationCode).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.ScanDateTime).IsUnicode(false);

                entity.HasOne(d => d.CartonNoNavigation)
                    .WithMany(p => p.CartonLocations)
                    .HasForeignKey(d => d.CartonNo)
                    .HasConstraintName("FK_CartonLocation_CartonStorage");
            });

            modelBuilder.Entity<CartonStorage>(entity =>
            {
                entity.Property(e => e.CartonNo).ValueGeneratedNever();

                entity.Property(e => e.AlternativeCartonNo).IsUnicode(false);

                entity.Property(e => e.CreatedUser).IsUnicode(false);

                entity.Property(e => e.LastConfirmedStatus).IsUnicode(false);

                entity.Property(e => e.LastConfirmeedRequestNo).IsUnicode(false);

                entity.Property(e => e.LastRequestNo).IsUnicode(false);

                entity.Property(e => e.LocationCode).IsUnicode(false);

                entity.Property(e => e.LuUser).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.HasOne(d => d.CartonTypeNavigation)
                    .WithMany(p => p.CartonStorages)
                    .HasForeignKey(d => d.CartonType)
                    .HasConstraintName("FK_CartonStorage_CartonType");

                entity.HasOne(d => d.DisposalTimeFrameNavigation)
                    .WithMany(p => p.CartonStorages)
                    .HasForeignKey(d => d.DisposalTimeFrame)
                    .HasConstraintName("FK_CartonStorage_DisposalTimeFrame");

                entity.HasOne(d => d.LastDeliveryRouteNavigation)
                    .WithMany(p => p.CartonStorages)
                    .HasForeignKey(d => d.LastDeliveryRoute)
                    .HasConstraintName("FK_CartonStorage_Route");
            });

            modelBuilder.Entity<CartonType>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Size).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_customer");

                entity.Property(e => e.AccountType).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.Address3).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.ContactAddress1).IsUnicode(false);

                entity.Property(e => e.ContactAddress2).IsUnicode(false);

                entity.Property(e => e.ContactAddress3).IsUnicode(false);

                entity.Property(e => e.ContactFax).IsUnicode(false);

                entity.Property(e => e.ContactName).IsUnicode(false);

                entity.Property(e => e.ContactPersonInv).IsUnicode(false);

                entity.Property(e => e.ContactTelephone1).IsUnicode(false);

                entity.Property(e => e.ContactTelephone2).IsUnicode(false);

                entity.Property(e => e.ContractNo).IsUnicode(false);

                entity.Property(e => e.DeliveryAddress1).IsUnicode(false);

                entity.Property(e => e.DeliveryAddress2).IsUnicode(false);

                entity.Property(e => e.DeliveryAddress3).IsUnicode(false);

                entity.Property(e => e.DeliveryFax).IsUnicode(false);

                entity.Property(e => e.DeliveryName).IsUnicode(false);

                entity.Property(e => e.DeliveryTelephone1).IsUnicode(false);

                entity.Property(e => e.DeliveryTelephone2).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Fax).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PickUpAddress1).IsUnicode(false);

                entity.Property(e => e.PickUpAddress2).IsUnicode(false);

                entity.Property(e => e.PickUpAddress3).IsUnicode(false);

                entity.Property(e => e.PickUpFax).IsUnicode(false);

                entity.Property(e => e.PickUpName).IsUnicode(false);

                entity.Property(e => e.PickUpTelephone1).IsUnicode(false);

                entity.Property(e => e.PickUpTelephone2).IsUnicode(false);

                entity.Property(e => e.PoNo).IsUnicode(false);

                entity.Property(e => e.SvatNo).IsUnicode(false);

                entity.Property(e => e.Telephone1).IsUnicode(false);

                entity.Property(e => e.Telephone2).IsUnicode(false);

                entity.Property(e => e.VatNo).IsUnicode(false);

                entity.Property(e => e.ZipCode).IsUnicode(false);

                entity.HasOne(d => d.BillingCycleNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BillingCycle)
                    .HasConstraintName("FK_customer_customerBillingCycle");

                entity.HasOne(d => d.RouteNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Route)
                    .HasConstraintName("FK_customer_customerRoute");

                entity.HasOne(d => d.ServiceProvidedNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ServiceProvided)
                    .HasConstraintName("FK_customer_serviceCategory");
            });

            modelBuilder.Entity<CustomerAuthorizationListBak>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.TrackingId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CustomerAuthorizationListDetail>(entity =>
            {
                entity.HasOne(d => d.Authorization)
                    .WithMany(p => p.CustomerAuthorizationListDetails)
                    .HasForeignKey(d => d.AuthorizationId)
                    .HasConstraintName("FK_CustomerAuthorizationListDetail_CustomerAuthorizationListHeader");

                entity.HasOne(d => d.LevelNavigation)
                    .WithMany(p => p.CustomerAuthorizationListDetails)
                    .HasForeignKey(d => d.Level)
                    .HasConstraintName("FK_CustomerAuthorizationListDetail_AuthorizationLevel");
            });

            modelBuilder.Entity<CustomerAuthorizationListHeader>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_customerAuthorizationList");

                entity.Property(e => e.ContactNo).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAuthorizationListHeaders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_customerAuthorizationList_customer");
            });

            modelBuilder.Entity<CustomerAuthorizationListHeaderTemp>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_customerAuthorizationListtemp");

                entity.Property(e => e.Email).IsUnicode(false);
            });

            modelBuilder.Entity<CutomerInvoiceProfile>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_cutomerInvoiceProfile");

                entity.Property(e => e.TrackingId).ValueGeneratedNever();

                entity.HasOne(d => d.InvoiceProfile)
                    .WithMany(p => p.CutomerInvoiceProfiles)
                    .HasForeignKey(d => d.InvoiceProfileId)
                    .HasConstraintName("FK_CutomerInvoiceProfile_InvoiceProfile");

                entity.HasOne(d => d.InvoiceProfileNavigation)
                    .WithMany(p => p.CutomerInvoiceProfiles)
                    .HasForeignKey(d => d.InvoiceProfileId)
                    .HasConstraintName("FK_CutomerInvoiceProfile_Customer");
            });

            modelBuilder.Entity<DisposalTimeFrame>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<InvoiceProfile>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<MenuRight>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_userMenuRights");

                entity.Property(e => e.MenuId).ValueGeneratedNever();
            });

            modelBuilder.Entity<MenuRightAttachedUser>(entity =>
            {
                entity.HasKey(e => e.UserMenuId)
                    .HasName("PK_userMenuRightsAttachedUser");

                entity.Property(e => e.UserMenuId).ValueGeneratedNever();
            });

            modelBuilder.Entity<MenuRightForm>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_menuRightForm");

                entity.Property(e => e.TrackingId).ValueGeneratedNever();

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuRightForms)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_userMenuRightsForms_userMenuRights");
            });

            modelBuilder.Entity<MenuRightFormUser>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_userMenuRightsUsersForms");

                entity.Property(e => e.TrackingId).ValueGeneratedNever();

                entity.HasOne(d => d.UserMenuTracking)
                    .WithMany(p => p.MenuRightFormUsers)
                    .HasForeignKey(d => d.UserMenuTrackingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRightFormUser_MenuRightAttachedUser");
            });

            modelBuilder.Entity<MenuRightUser>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_userMenuRightsUsers");

                entity.Property(e => e.TrackingId).ValueGeneratedNever();

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuRightUsers)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRightUser_menuRight");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MenuRightUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRightUser_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<SlabTypeDetail>(entity =>
            {
                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.SlabTypeDetails)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_SlabTypeDetail_SlabTypeHeader");
            });

            modelBuilder.Entity<SlabTypeHeader>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_SlabType");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.InvoiceProfile)
                    .WithMany(p => p.SlabTypeHeaders)
                    .HasForeignKey(d => d.InvoiceProfileId)
                    .HasConstraintName("FK_SlabTypeHeader_InvoiceProfile");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.EmpId).IsUnicode(false);

                entity.Property(e => e.UserFullName).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<UserActivityLogger>(entity =>
            {
                entity.Property(e => e.ActivityLog).IsUnicode(false);

                entity.Property(e => e.ActivityType).IsUnicode(false);
            });

            modelBuilder.Entity<UserActivityType>(entity =>
            {
                entity.Property(e => e.ActivityId).ValueGeneratedNever();

                entity.Property(e => e.ActivityDescription).IsUnicode(false);
            });

            modelBuilder.Entity<UserLogger>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_userLogger");
            });

            modelBuilder.Entity<UserPassword>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPasswords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserPassword_UserPassword");
            });
            modelBuilder.Entity<CustomerSearch>();
            modelBuilder.Entity<CartonStorageSearch>();
            modelBuilder.Entity<BoolReturn>().HasNoKey();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
