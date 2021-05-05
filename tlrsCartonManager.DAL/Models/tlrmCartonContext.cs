using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tlrsCartonManager.DAL.Dtos.Ownership;
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Models.Docket;
using tlrsCartonManager.DAL.Models.Invoice;
using tlrsCartonManager.DAL.Models.MetaData;
using tlrsCartonManager.DAL.Models.Operation;
using tlrsCartonManager.DAL.Models.Ownership;
using tlrsCartonManager.DAL.Models.Pick;

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
        public virtual DbSet<CustomerAuthorizationListDetail> CustomerAuthorizationListDetails { get; set; }
        public virtual DbSet<CustomerAuthorizationListHeader> CustomerAuthorizationListHeaders { get; set; }
        public virtual DbSet<CustomerRightUser> CustomerRightUsers { get; set; }
        public virtual DbSet<CutomerInvoiceProfile> CutomerInvoiceProfiles { get; set; }
        public virtual DbSet<DisposalTimeFrame> DisposalTimeFrames { get; set; }
        public virtual DbSet<InvoiceConfirmation> InvoiceConfirmations { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public virtual DbSet<InvoiceProfile> InvoiceProfiles { get; set; }
        public virtual DbSet<InvoiceProfileold> InvoiceProfileolds { get; set; }
        public virtual DbSet<InvoiceSlabTypeDetail> InvoiceSlabTypeDetails { get; set; }
        public virtual DbSet<InvoiceSlabTypeHeader> InvoiceSlabTypeHeaders { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MenuRight> MenuRights { get; set; }
        public virtual DbSet<MenuRightAttachedUser> MenuRightAttachedUsers { get; set; }
        public virtual DbSet<MenuRightForm> MenuRightForms { get; set; }
        public virtual DbSet<MenuRightFormUser> MenuRightFormUsers { get; set; }
        public virtual DbSet<MenuRightUser> MenuRightUsers { get; set; }
        public virtual DbSet<RequestDetail> RequestDetails { get; set; }
        public virtual DbSet<RequestHeader> RequestHeaders { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Sequence> Sequences { get; set; }
        public virtual DbSet<SequenceType> SequenceTypes { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<StorageType> StorageTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActivityLogger> UserActivityLoggers { get; set; }
        public virtual DbSet<UserActivityType> UserActivityTypes { get; set; }
        public virtual DbSet<UserLogger> UserLoggers { get; set; }
        public virtual DbSet<UserPassword> UserPasswords { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WorkOrderRequestType> WorkOrderRequestTypes { get; set; }
        public virtual DbSet<PickList> PickLists { get; set; }
        public virtual DbSet<CartonOwnerShip> CartonOwnerShips { get; set; }
        public virtual DbSet<ReceiveType> ReceiveTypes { get; set; }
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

                entity.Property(e => e.LastConfirmedRequestNo).IsUnicode(false);

                entity.Property(e => e.LastConfirmedStatus).IsUnicode(false);

                entity.Property(e => e.LastRequestNo).IsUnicode(false);

                entity.Property(e => e.LocationCode).IsUnicode(false);

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

            modelBuilder.Entity<CustomerRightUser>(entity =>
            {
                entity.Property(e => e.CustomerCode).IsUnicode(false);
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

            modelBuilder.Entity<InvoiceConfirmation>(entity =>
            {
                entity.Property(e => e.AlternativeCartonNo).IsUnicode(false);

                entity.Property(e => e.CartonNo).IsUnicode(false);

                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.CustomerCode).IsUnicode(false);

                entity.Property(e => e.LastRequestNo).IsUnicode(false);

                entity.Property(e => e.WoType).IsUnicode(false);
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.Property(e => e.CustomerCode).IsUnicode(false);

                entity.Property(e => e.Descripton).IsUnicode(false);

                entity.Property(e => e.InvoiceId).IsUnicode(false);

                entity.Property(e => e.RateType).IsUnicode(false);

                entity.Property(e => e.RequestType).IsUnicode(false);

                entity.Property(e => e.WoType).IsUnicode(false);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_InvoiceDetail_InvoiceHeader");
            });

            modelBuilder.Entity<InvoiceHeader>(entity =>
            {
                entity.Property(e => e.InvoiceId).IsUnicode(false);

                entity.Property(e => e.LuUserId).IsUnicode(false);

                entity.Property(e => e.TrackingId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<InvoiceProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK_InvoiceProfile_1");

                entity.Property(e => e.ProfileDesc).IsUnicode(false);
            });

            modelBuilder.Entity<InvoiceProfileold>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<InvoiceSlabTypeDetail>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_SlabTypeDetail");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.InvoiceSlabTypeDetails)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_SlabTypeDetail_SlabTypeHeader");
            });

            modelBuilder.Entity<InvoiceSlabTypeHeader>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_SlabType");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.InvoiceProfile)
                    .WithMany(p => p.InvoiceSlabTypeHeaders)
                    .HasForeignKey(d => d.InvoiceProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlabTypeHeader_InvoiceProfile");
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

            modelBuilder.Entity<RequestDetail>(entity =>
            {
                entity.HasKey(e => e.TrackingId)
                    .HasName("PK_Request_Allocation");

                entity.Property(e => e.FeedBack).IsUnicode(false);

                entity.Property(e => e.PickListNo).IsUnicode(false);

                entity.Property(e => e.RequestNo).IsUnicode(false);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestDetail_RequestHeader");
            });

            modelBuilder.Entity<RequestHeader>(entity =>
            {
                entity.Property(e => e.AuthorizedOfficer).IsUnicode(false);

                entity.Property(e => e.ContactPersonName).IsUnicode(false);

                entity.Property(e => e.CustomerReference).IsUnicode(false);

                entity.Property(e => e.DeliveryLocation).IsUnicode(false);

                entity.Property(e => e.DeliveryRoute).IsUnicode(false);

                entity.Property(e => e.DeviceId).IsUnicode(false);

                entity.Property(e => e.MobileRequestNo).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);              

                entity.Property(e => e.Reminder1).IsUnicode(false);

                entity.Property(e => e.Reminder2).IsUnicode(false);

                entity.Property(e => e.Reminder3).IsUnicode(false);

                entity.Property(e => e.RequestNo).IsUnicode(false);

                entity.Property(e => e.RequestType).IsUnicode(false);

                entity.Property(e => e.ServiceType).IsUnicode(false);

                entity.Property(e => e.WorkOrderType).IsUnicode(false);
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.HasKey(e => e.TypeCode)
                    .HasName("PK_RequestType_1");

                entity.Property(e => e.TypeCode).IsUnicode(false);
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

            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.HasKey(e => e.SequenceType)
                    .HasName("PK_Sequences");

                entity.Property(e => e.SequenceType).IsUnicode(false);

                entity.Property(e => e.CurrentSuffix).IsUnicode(false);

                entity.Property(e => e.RequestTypeCode).IsUnicode(false);
            });

            modelBuilder.Entity<SequenceType>(entity =>
            {
                entity.HasKey(e => e.TypeCode)
                    .HasName("PK_SequenceRequestType");

                entity.Property(e => e.TypeCode).IsUnicode(false);
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.WoType).IsUnicode(false);
            });

            modelBuilder.Entity<StorageType>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Size).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);
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

            modelBuilder.Entity<WorkOrderRequestType>(entity =>
            {
                entity.Property(e => e.TypeCode).IsUnicode(false);

                entity.Property(e => e.RequestTypeCode).IsUnicode(false);

                entity.HasOne(d => d.RequestTypeCodeNavigation)
                    .WithMany(p => p.WorkOrderRequestTypes)
                    .HasForeignKey(d => d.RequestTypeCode)
                    .HasConstraintName("FK_WorkOrderRequestType_RequestType");
            });


            modelBuilder.Entity<PickList>(entity =>
            {
                entity.Property(e => e.Barcode).IsUnicode(false);

                entity.Property(e => e.LastSentDeviceId).IsUnicode(false);

                entity.Property(e => e.LocationCode).IsUnicode(false);

                entity.Property(e => e.PickListNo).IsUnicode(false);

                entity.Property(e => e.RequestNo).IsUnicode(false);

                entity.Property(e => e.WareHouseCode).IsUnicode(false);
            });

            modelBuilder.Entity<CartonOwnerShip>(entity =>
            {
                entity.Property(e => e.FromCustomerCode).IsUnicode(false);

                entity.Property(e => e.OwnershipChangedBy).IsUnicode(false);

                entity.Property(e => e.ToCustomerCode).IsUnicode(false);
            });
            modelBuilder.Entity<ReceiveType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<MobileDevice>(entity =>
            {
                entity.HasKey(e => e.DeviceCode)
                    .HasName("PK_Mobile-Devices");

                entity.Property(e => e.DeviceCode).IsUnicode(false);

                entity.Property(e => e.DeviceName).IsUnicode(false);

                entity.Property(e => e.LastSynchedUser).IsUnicode(false);
            });
            modelBuilder.Entity<CustomerSearch>();
            modelBuilder.Entity<CartonStorageSearch>();
            modelBuilder.Entity<UserSearch>();
            modelBuilder.Entity<RequestSearch>();
            modelBuilder.Entity<InvoiceSearch>();
            modelBuilder.Entity<InvoiceConfirmationSearch>();
            modelBuilder.Entity<InvoiceConfirmationDetail>();            
            modelBuilder.Entity<CartonInquiry>();

            modelBuilder.Entity<PickListSearch>().HasNoKey();
            modelBuilder.Entity<BoolReturn>().HasNoKey();
            modelBuilder.Entity<StringReturn>().HasNoKey();
            modelBuilder.Entity<TableReturn>().HasNoKey();
            modelBuilder.Entity<InvoiceReturn>();
            modelBuilder.Entity<DocketPrintDetail>().HasNoKey();
            modelBuilder.Entity<CartonRequest>().HasNoKey();
            modelBuilder.Entity<OperationOverviewByWoType>().HasNoKey();
            modelBuilder.Entity<OperationOverviewByUserLocaion>().HasNoKey();
            modelBuilder.Entity<CartonOwnershipSearch>().HasNoKey();
            modelBuilder.Entity<CartonSummary>().HasNoKey();
            modelBuilder.Entity<CartonUserSummary>().HasNoKey();
            modelBuilder.Entity<CartonLocationSummary>().HasNoKey();
            modelBuilder.Entity<RequestedDetail>().HasNoKey();
            modelBuilder.Entity<CartonOwnershipSummary>().HasNoKey();
            modelBuilder.Entity<PickListPendingListItem>().HasNoKey();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
