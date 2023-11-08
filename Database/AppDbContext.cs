using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Truck1.Model;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Confirmedbookedtruckdetail> Confirmedbookedtruckdetails { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customerfeedback> Customerfeedbacks { get; set; }

    public virtual DbSet<Customeroffer> Customeroffers { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Helpdeskquery> Helpdeskqueries { get; set; }

    public virtual DbSet<Livetrackstatus> Livetrackstatuses { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Modeofpayment> Modeofpayments { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Offerdetail> Offerdetails { get; set; }

    public virtual DbSet<Paymentdetail> Paymentdetails { get; set; }

    public virtual DbSet<Paymentstatus> Paymentstatuses { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Truckbooking> Truckbookings { get; set; }

    public virtual DbSet<Truckfacility> Truckfacilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database= TRUCK;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Confirmedbookedtruckdetail>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("confirmedbookedtruckdetails_pkey");

            entity.ToTable("confirmedbookedtruckdetails");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Bookingdate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bookingdate");
            entity.Property(e => e.Bookingpersonidproof)
                .HasMaxLength(255)
                .HasColumnName("bookingpersonidproof");
            entity.Property(e => e.Companylicensenumber)
                .HasMaxLength(255)
                .HasColumnName("companylicensenumber");
            entity.Property(e => e.Conveniencerequested).HasColumnName("conveniencerequested");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Productdamagecovered).HasColumnName("productdamagecovered");
            entity.Property(e => e.Productdetails).HasColumnName("productdetails");
            entity.Property(e => e.Productlicensenumber)
                .HasMaxLength(255)
                .HasColumnName("productlicensenumber");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Truckid).HasColumnName("truckid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Customer).WithMany(p => p.Confirmedbookedtruckdetails)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("confirmedbookedtruckdetails_customerid_fkey");

            entity.HasOne(d => d.Truck).WithMany(p => p.Confirmedbookedtruckdetails)
                .HasForeignKey(d => d.Truckid)
                .HasConstraintName("confirmedbookedtruckdetails_truckid_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countryid).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Countryname)
                .HasMaxLength(255)
                .HasColumnName("countryname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Canbooktruck).HasColumnName("canbooktruck");
            entity.Property(e => e.Contact)
                .HasMaxLength(20)
                .HasColumnName("contact");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Idproof)
                .HasMaxLength(255)
                .HasColumnName("idproof");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Organizationname)
                .HasMaxLength(255)
                .HasColumnName("organizationname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Customerfeedback>(entity =>
        {
            entity.HasKey(e => e.Feedbackid).HasName("customerfeedback_pkey");

            entity.ToTable("customerfeedback");

            entity.Property(e => e.Feedbackid).HasColumnName("feedbackid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Feedbacktext).HasColumnName("feedbacktext");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customerfeedbacks)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("customerfeedback_customerid_fkey");
        });

        modelBuilder.Entity<Customeroffer>(entity =>
        {
            entity.HasKey(e => e.Customerofferid).HasName("customeroffer_pkey");

            entity.ToTable("customeroffer");

            entity.Property(e => e.Customerofferid).HasColumnName("customerofferid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Offerapplied).HasColumnName("offerapplied");
            entity.Property(e => e.Offerid).HasColumnName("offerid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customeroffers)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("customeroffer_customerid_fkey");

            entity.HasOne(d => d.Offer).WithMany(p => p.Customeroffers)
                .HasForeignKey(d => d.Offerid)
                .HasConstraintName("customeroffer_offerid_fkey");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Districtid).HasName("district_pkey");

            entity.ToTable("district");

            entity.Property(e => e.Districtid).HasColumnName("districtid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Districtname)
                .HasMaxLength(255)
                .HasColumnName("districtname");
            entity.Property(e => e.Stateid).HasColumnName("stateid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.State).WithMany(p => p.Districts)
                .HasForeignKey(d => d.Stateid)
                .HasConstraintName("district_stateid_fkey");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("documents_pkey");

            entity.ToTable("documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExcelPath).HasColumnName("excel_path");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.PdfPath).HasColumnName("pdf_path");
        });

        modelBuilder.Entity<Helpdeskquery>(entity =>
        {
            entity.HasKey(e => e.Queryid).HasName("helpdeskqueries_pkey");

            entity.ToTable("helpdeskqueries");

            entity.Property(e => e.Queryid).HasColumnName("queryid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Querydate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("querydate");
            entity.Property(e => e.Querytext).HasColumnName("querytext");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Customer).WithMany(p => p.Helpdeskqueries)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("helpdeskqueries_customerid_fkey");
        });

        modelBuilder.Entity<Livetrackstatus>(entity =>
        {
            entity.HasKey(e => e.Trackingid).HasName("livetrackstatus_pkey");

            entity.ToTable("livetrackstatus");

            entity.Property(e => e.Trackingid).HasColumnName("trackingid");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Currentlocation).HasColumnName("currentlocation");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Booking).WithMany(p => p.Livetrackstatuses)
                .HasForeignKey(d => d.Bookingid)
                .HasConstraintName("livetrackstatus_bookingid_fkey");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Locationid).HasName("location_pkey");

            entity.ToTable("location");

            entity.Property(e => e.Locationid).HasColumnName("locationid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Districtid).HasColumnName("districtid");
            entity.Property(e => e.Locationtype)
                .HasMaxLength(255)
                .HasColumnName("locationtype");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.District).WithMany(p => p.Locations)
                .HasForeignKey(d => d.Districtid)
                .HasConstraintName("location_districtid_fkey");
        });

        modelBuilder.Entity<Modeofpayment>(entity =>
        {
            entity.HasKey(e => e.Modeofpaymentid).HasName("modeofpayment_pkey");

            entity.ToTable("modeofpayment");

            entity.Property(e => e.Modeofpaymentid).HasColumnName("modeofpaymentid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Paymentdescription)
                .HasMaxLength(255)
                .HasColumnName("paymentdescription");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Notificationid).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Notificationid).HasColumnName("notificationid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Notificationdate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("notificationdate");
            entity.Property(e => e.Notificationtext).HasColumnName("notificationtext");
            entity.Property(e => e.Notificationtype)
                .HasMaxLength(255)
                .HasColumnName("notificationtype");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Customer).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("notifications_customerid_fkey");
        });

        modelBuilder.Entity<Offerdetail>(entity =>
        {
            entity.HasKey(e => e.Offerid).HasName("offerdetails_pkey");

            entity.ToTable("offerdetails");

            entity.Property(e => e.Offerid).HasColumnName("offerid");
            entity.Property(e => e.Applicablelocationtype)
                .HasMaxLength(255)
                .HasColumnName("applicablelocationtype");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Minimummonthlybookings).HasColumnName("minimummonthlybookings");
            entity.Property(e => e.Offerpercentage).HasColumnName("offerpercentage");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Paymentdetail>(entity =>
        {
            entity.HasKey(e => e.Paymentdetailsid).HasName("paymentdetails_pkey");

            entity.ToTable("paymentdetails");

            entity.Property(e => e.Paymentdetailsid).HasColumnName("paymentdetailsid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Fasttrackcost).HasColumnName("fasttrackcost");
            entity.Property(e => e.Festivaltimecost).HasColumnName("festivaltimecost");
            entity.Property(e => e.Loadcapacitybasedcost).HasColumnName("loadcapacitybasedcost");
            entity.Property(e => e.Locationbasedcost).HasColumnName("locationbasedcost");
            entity.Property(e => e.Modeofpaymentid).HasColumnName("modeofpaymentid");
            entity.Property(e => e.Totalcost).HasColumnName("totalcost");
            entity.Property(e => e.Travelwithloadcost).HasColumnName("travelwithloadcost");
            entity.Property(e => e.Truckchoicecost).HasColumnName("truckchoicecost");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Modeofpayment).WithMany(p => p.Paymentdetails)
                .HasForeignKey(d => d.Modeofpaymentid)
                .HasConstraintName("paymentdetails_modeofpaymentid_fkey");
        });

        modelBuilder.Entity<Paymentstatus>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("paymentstatus_pkey");

            entity.ToTable("paymentstatus");

            entity.Property(e => e.Paymentid).HasColumnName("paymentid");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Discountapplied).HasColumnName("discountapplied");
            entity.Property(e => e.Paymentamount).HasColumnName("paymentamount");
            entity.Property(e => e.Paymentdetailsid).HasColumnName("paymentdetailsid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Booking).WithMany(p => p.Paymentstatuses)
                .HasForeignKey(d => d.Bookingid)
                .HasConstraintName("paymentstatus_bookingid_fkey");

            entity.HasOne(d => d.Paymentdetails).WithMany(p => p.Paymentstatuses)
                .HasForeignKey(d => d.Paymentdetailsid)
                .HasConstraintName("paymentstatus_paymentdetailsid_fkey");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Stateid).HasName("state_pkey");

            entity.ToTable("state");

            entity.Property(e => e.Stateid).HasColumnName("stateid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Statename)
                .HasMaxLength(255)
                .HasColumnName("statename");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("state_countryid_fkey");
        });

        modelBuilder.Entity<Truckbooking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("truckbooking_pkey");

            entity.ToTable("truckbooking");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Bookingpersonidproof)
                .HasMaxLength(255)
                .HasColumnName("bookingpersonidproof");
            entity.Property(e => e.Companylicensenumber)
                .HasMaxLength(255)
                .HasColumnName("companylicensenumber");
            entity.Property(e => e.Conveniencerequested).HasColumnName("conveniencerequested");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Deliverylocationid).HasColumnName("deliverylocationid");
            entity.Property(e => e.IsProductdamagecovered).HasColumnName("is_productdamagecovered");
            entity.Property(e => e.Pickuplocationid).HasColumnName("pickuplocationid");
            entity.Property(e => e.Productdetails).HasColumnName("productdetails");
            entity.Property(e => e.Productlicensenumber)
                .HasMaxLength(255)
                .HasColumnName("productlicensenumber");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Truckid).HasColumnName("truckid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Customer).WithMany(p => p.Truckbookings)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("truckbooking_customerid_fkey");

            entity.HasOne(d => d.Deliverylocation).WithMany(p => p.TruckbookingDeliverylocations)
                .HasForeignKey(d => d.Deliverylocationid)
                .HasConstraintName("truckbooking_deliverylocationid_fkey");

            entity.HasOne(d => d.Pickuplocation).WithMany(p => p.TruckbookingPickuplocations)
                .HasForeignKey(d => d.Pickuplocationid)
                .HasConstraintName("truckbooking_pickuplocationid_fkey");

            entity.HasOne(d => d.Truck).WithMany(p => p.Truckbookings)
                .HasForeignKey(d => d.Truckid)
                .HasConstraintName("truckbooking_truckid_fkey");
        });

        modelBuilder.Entity<Truckfacility>(entity =>
        {
            entity.HasKey(e => e.Truckid).HasName("truckfacility_pkey");

            entity.ToTable("truckfacility");

            entity.Property(e => e.Truckid).HasColumnName("truckid");
            entity.Property(e => e.Acnonac).HasColumnName("acnonac");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Fasttrack).HasColumnName("fasttrack");
            entity.Property(e => e.IsAvailable).HasColumnName("is_available");
            entity.Property(e => e.Loadcapacity).HasColumnName("loadcapacity");
            entity.Property(e => e.Locationid).HasColumnName("locationid");
            entity.Property(e => e.NonAc).HasColumnName("non_ac");
            entity.Property(e => e.Tollgateapplicable).HasColumnName("tollgateapplicable");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Location).WithMany(p => p.Truckfacilities)
                .HasForeignKey(d => d.Locationid)
                .HasConstraintName("truckfacility_locationid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
