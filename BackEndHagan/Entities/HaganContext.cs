using System;
using System.Collections.Generic;
using BackEndHagan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using QHijin.Entities;

namespace BackEndHagan.Entities;

public partial class HaganContext : IdentityDbContext<ApplicationUser>
{
    public HaganContext()
    {
    }

    public HaganContext(DbContextOptions<HaganContext> options)
        : base(options)
    {
    }

    #region Entities
    public DbSet<About> Abouts { get; set; }
    public  DbSet<Admin> Admins { get; set; }
    public  DbSet<ItemPhysical> ItemPhysical { get; set; }
    public  DbSet<Banner> Banners { get; set; }
    public  DbSet<Advantages> Advantages { get; set; }
    public  DbSet<HowTobuy> HowTobuy { get; set; }
    public  DbSet<Feedback> Feedback { get; set; }
    public  DbSet<Contracting_Policy> Contracting_Policy { get; set; }
    public  DbSet<Policy_Refund> Policy_Refund { get; set; }
    public  DbSet<Delivery_Period> Delivery_Period { get; set; }
    public  DbSet<AboutPhotos> AboutPhoto { get; set; }
    public  DbSet<PaymentRequest> PaymentRequest { get; set; }
    public  DbSet<Ads> Ads { get; set; }
    public  DbSet<ContactUs> ContactUs { get; set; }
    public  DbSet<Work> Work { get; set; }
    public  DbSet<Trainers> Trainer { get; set; } 
    public  DbSet<QHijin.Entities.Action> Actions { get; set; }
    public  DbSet<Type> Types { get; set; }
    public  DbSet<Bidding> Biddings { get; set; }
    public  DbSet<ServicesSite> Services { get; set; }
    public  DbSet<Employee> Employees { get; set; }
    public  DbSet<Photo> Photos { get; set; }
    public  DbSet<EmployeeItem> EmployeeItems { get; set; }
    public  DbSet<Invoice> Invoices { get; set; }
    public  DbSet<Item> Items { get; set; }
    public  DbSet<Contact> Contacts { get; set; }
    public  DbSet<PriceAndRate> PriceAndRates { get; set; }
    public  DbSet<PrivacyAndPolicy> PrivacyAndPolicies { get; set; }
    public  DbSet<Salary> Salaries { get; set; }
    public  DbSet<SocialMedia> SocialMedia { get; set; }
    public  DbSet<TermsAndCondition> TermsAndConditions { get; set; }
    public  DbSet<Title> Titles { get; set; }
    public  DbSet<User> Users { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    #endregion
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser> (entity =>
        {
       
            entity.Property (e => e.PasswordHash)
           .HasMaxLength (450);
            entity.HasIndex (u => u.PassportNumber)
            .IsUnique ();
        });

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.AboutNo).HasName("PK__About__AB3FDC2C325B1618");

            entity.ToTable("About");

            entity.Property(e => e.AboutNo)
                .ValueGeneratedOnAdd()
                .HasColumnName("aboutNo");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.TextAr)
                .IsUnicode(true)
                .HasColumnName("textAr");
            entity.Property (e => e.TextEn)
             .IsUnicode (true)
             .HasColumnName ("textEn");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("title");

            entity.HasOne(d => d.Emp).WithMany(p => p.Abouts)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__About__empID__48CFD27E");
        });

        modelBuilder.Entity<Photo> ()
        .HasOne (p => p.item)
        .WithMany (i => i.Photo)
        .HasForeignKey (p => p.ItemId);

        modelBuilder.Entity<AboutPhotos>().HasOne(d => d.About).WithMany(p => p.AboutPhoto)
                .HasForeignKey(d => d.AboutId);

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admin");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.HasKey (e => e.Id);
            entity.Property (e => e.Id)
                .ValueGeneratedOnAdd ()
                .HasColumnName ("Id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("email");
            entity.Property (e => e.PasswordHash)
            .HasMaxLength (450)
            .HasColumnName ("PasswordHash");
            entity.HasOne (d => d.Emp).WithMany ()
            .HasForeignKey (a => a.EmpId);
            entity.HasOne (d => d.Titles).WithMany ()
            .HasForeignKey (a => a.TitleId);
        });

  
        modelBuilder.Entity<ItemPhysical> (entity =>
        {
            entity.Property (e => e.foot)
                .IsUnicode (true)
                .HasColumnName ("foot");

            entity.Property (e => e.eye)
            .IsUnicode (true)
            .HasColumnName ("eye");

            entity.Property (e => e.front)
            .IsUnicode (true)
            .HasColumnName ("front");

            entity.Property (e => e.back)
          .IsUnicode (true)
          .HasColumnName ("back");

            entity.Property (e => e.foot)
        .IsUnicode (true)
        .HasColumnName ("foot");
            entity.HasOne (x => x.Item)
            .WithOne (x => x.ItemPhysical)
            .HasForeignKey<ItemPhysical> (x => x.ItemId);

        });
            
        modelBuilder.Entity<Work> (entity =>
        {
            entity.HasKey (e => e.Id);
            entity.Property (e => e.Id)
                .ValueGeneratedOnAdd ()
                .HasColumnName ("Id");
            entity.Property (e => e.EmpId).HasColumnName ("empID");
            entity.Property (e => e.TitleId).HasColumnName ("TitleId");
            entity.HasOne (d => d.Emp).WithMany (p => p.Works)
    .HasForeignKey (d => d.EmpId);
            entity.HasOne (d => d.Title).WithMany (p => p.Works)
                .HasForeignKey (d => d.TitleId);
        });
        modelBuilder.Entity<QHijin.Entities.Action> (entity =>
        {
            entity.HasKey (e => e.Id);
            entity.Property (e => e.Id)
                .ValueGeneratedOnAdd ()
                .HasColumnName ("Id");
            entity.Property (e => e.UserId).HasColumnName ("UserID");
            entity.Property (e => e.TypeId).HasColumnName ("TypeId");
            entity.HasOne (d => d.User).WithMany (p => p.Actions)
    .HasForeignKey (d =>d.UserId);
            entity.HasOne (d => d.Type).WithMany (p => p.Actions)
                .HasForeignKey (d => d.TypeId);
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BanarNo).HasName("PK__Banners__E1C7EEED86D5AABA");

            entity.Property(e => e.BanarNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("banarNo");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.SubTitleAr)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("SubTitleAr");
            entity.Property (e => e.SubTitleEn)
            .HasMaxLength (100)
            .IsUnicode (true)
            .HasColumnName ("SubTitleEn");

            entity.HasOne(d => d.Emp).WithMany(p => p.Banners)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__Banners__empID__5441852A");
        });

        modelBuilder.Entity<Bidding>(entity =>
        {
            entity.HasKey(e => e.BiddingNo).HasName("PK__Biddings__B58ECEC7767E4C79");

            entity.Property(e => e.BiddingNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("biddingNo");
            entity.Property(e => e.ItemNo).HasColumnName("itemNo");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.HasOne (d => d.User).WithMany (p => p.Biddings)
               .HasForeignKey (d => d.UserId)
               .HasConstraintName ("FK__Biddings__userID__32E0915F");
            entity.HasOne (d => d.Items).WithOne (p => p.BiddingNoNavigation)
            .HasForeignKey<Item> (i => i.Id);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AFB3EC6D642F11E3");
            entity.Property(e => e.EmpId)
                .ValueGeneratedOnAdd ()
                .HasColumnName("empID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("fullName");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(true)
                .HasColumnName("phone");
            entity.HasIndex (u => u.Email).IsUnique ();
            
        });

        modelBuilder.Entity<EmployeeItem>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.ItemId }).HasName("PK__Employee__1AD9FEE9A85FDFFB");
            entity.ToTable("EmployeeItem");

            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.Doctor)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("doctor");

            entity.HasOne(d => d.Emp).WithMany(p => p.EmployeeItems)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeI__empID__3F466844");

            entity.HasOne(d => d.Item).WithMany(p => p.EmployeeItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeI__itemI__403A8C7D");
        });

        modelBuilder.Entity<Type> (entity =>
        {
            entity.Property (e => e.TypeId)
            .ValueGeneratedOnAdd ().HasColumnName ("TypeId");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__1252410C2EB72C9C");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedOnAdd ()
                .HasColumnName("invoiceID");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("dateTime");
            entity.Property (e => e.InvoiceNo)
                .HasMaxLength (20)
                .IsUnicode (true)
                .HasColumnName ("invoiceNo").ValueGeneratedNever ();

            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(true)
                .HasColumnName("type");
            entity.HasOne(d => d.Item).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Invoice__itemID__3A81B327");

            entity.Property (e => e.UserId).HasColumnName ("userID");
            entity.HasOne (d => d.User).WithMany (p => p.Invoices)
                .HasForeignKey (d => d.UserId).OnDelete (DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Items__3213E83F9D190F1F");
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd ()
                .HasColumnName("id");
            entity.Property (e => e.BiddingNo).HasColumnName ("biddingNo");
            entity.Property(e => e.CheckedDate)
                .HasColumnType("datetime")
                .HasColumnName("checkedDate");
            entity.Property(e => e.Disc)
                .IsUnicode(true)
                .HasColumnName("disc");
            entity.Property(e => e.Health)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("health");
            entity.Property(e => e.History)
                .IsUnicode(true)
                .HasColumnName("history");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.HasOne (d => d.BiddingNoNavigation).WithOne (p => p.Items).HasForeignKey<Bidding>(e=>e.ItemNo);
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.HasOne(d => d.User).WithMany(p => p.Items)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Items__userID__35BCFE0A");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactNo).HasName("PK__Contacts__F5CD605BCDDC429C");

            entity.Property(e => e.ContactNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("ContactNo");
            entity.Property(e => e.ContactMsg)
                .IsUnicode(true)
                .HasColumnName("ContactMsg");
            entity.Property (e => e.ReadTime)
             .IsUnicode (true)
             .HasColumnName ("ReadTime");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Mails__userID__300424B4");
        });

        modelBuilder.Entity<PriceAndRate>(entity =>
        {
            entity.HasKey(e => e.PrNo).HasName("PK__PriceAnd__46655E2E38A279F4");

            entity.ToTable("PriceAndRate");

            entity.Property(e => e.PrNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("prNo");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property (e => e.TextEn)
                .IsUnicode (true)
                .HasColumnName ("TextEn");
            entity.Property (e => e.TextAr)
             .IsUnicode (true)
             .HasColumnName ("TextAr");

            entity.HasOne(d => d.Emp).WithMany(p => p.PriceAndRates)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__PriceAndR__empID__4E88ABD4");
        });

        modelBuilder.Entity<Ads>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__AdIdAnd__46655E2E38A279F4");
            entity.ToTable("Ads");
            entity.Property(e => e.AdId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AdId");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.Text)
                .IsUnicode(true)
                .HasColumnName("Text");
            entity.Property(e => e.AdsImgLocalPath)
             .IsUnicode(true)
             .HasColumnName("AdsImgLocalPath");
            entity.Property(e => e.AdsType)
             .IsUnicode(true)
             .HasColumnName("AdsType");
            entity.Property(e => e.Link)
             .IsUnicode(true)
             .HasColumnName("Link");
            entity.HasOne(d => d.Emp).WithMany(p => p.Ads)
                .HasForeignKey(d => d.EmpId);
        });

        modelBuilder.Entity<PaymentRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.ToTable("PaymentRequest");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");
            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.ItemId).HasColumnName("ItemId");
          
            entity.Property(e => e.SendDate)
             .IsUnicode(true)
             .HasColumnName("SendDate");
            entity.Property(e => e.RequestRef)
           .IsUnicode(true)
           .HasColumnName("RequestRef");
            entity.Property(e => e.PaymentCurrency)
             .IsUnicode(true)
             .HasColumnName("PaymentCurrency");
            entity.Property(e => e.PaymentDone)
            .HasColumnName("PaymentDone");
            entity.Property(e => e.PaymentAmount)
              .HasColumnName("PaymentAmount");
            entity.Property(e => e.PaymentDate)
            .HasColumnName("PaymentDate");
            entity.Property(e => e.CountryName)
             .IsUnicode(true)
             .HasColumnName("CountryName");
            entity.Property(e => e.EmailAddress)
          .IsUnicode(true)
          .HasColumnName("EmailAddress");
            entity.Property(e => e.PhoneNo)
            .HasColumnName("PhoneNo");
            entity.Property(e => e.FirstName)
            .HasColumnName("FirstName");
            entity.Property(e => e.LastName)
            .HasColumnName("LastName");
            entity.Property(e => e.OrderDetails)
            .HasColumnName("OrderDetails");
            entity.HasOne(d => d.User).WithMany(p => p.PaymentRequest)
                .HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict); ;
            entity.HasOne(d => d.Item).WithMany(p => p.PaymentRequest)
                .HasForeignKey(d => d.ItemId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.Trainer).WithMany(p => p.PaymentRequest)
                .HasForeignKey(d => d.TrainerId).OnDelete(DeleteBehavior.Restrict); ;
        });


        modelBuilder.Entity<ServicesSite>(entity =>
        {
            entity.ToTable("ServicesSite");
            entity.Property(e => e.SerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SerId");
            entity.Property(e => e.ParphEn)
         .IsUnicode(true)
         .HasColumnName("ParphEn");
            entity.Property(e => e.ParphAr)
        .IsUnicode(true)
        .HasColumnName("ParphAr");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.SubTitleEn)
                .IsUnicode(true)
                .HasColumnName("SubTitleEn");
            entity.Property(e => e.SubTitleAr)
             .IsUnicode(true)
             .HasColumnName("SubTitleAr");
            entity.HasOne(d => d.Emp).WithMany(p => p.Services)
                .HasForeignKey(d => d.EmpId);
        });

        modelBuilder.Entity<ContactUs>(entity =>
        {
            entity.ToTable("ContactUs");

            entity.Property(e => e.ContId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ContId");
            entity.Property(e => e.TextEn)
         .IsUnicode(true)
         .HasColumnName("TextEn");
            entity.Property(e => e.TextAr)
        .IsUnicode(true)
        .HasColumnName("TextAr");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.HasOne(d => d.Emp).WithMany(p => p.ContactUs)
                .HasForeignKey(d => d.EmpId);
        });

        modelBuilder.Entity<Advantages>(entity =>
        {
            entity.ToTable("Advantages");

            entity.Property(e => e.AdvId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AdvId");
            entity.Property(e => e.TextEn)
         .IsUnicode(true)
         .HasColumnName("TextEn");
            entity.Property(e => e.TextAr)
        .IsUnicode(true)
        .HasColumnName("TextAr");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.HasOne(d => d.Emp).WithMany(p => p.Advantages)
                .HasForeignKey(d => d.EmpId);
        });

        modelBuilder.Entity<Contracting_Policy>(entity =>
        {
            entity.ToTable("Contracting_Policy");

            entity.Property(e => e.con_pNo)
                .ValueGeneratedOnAdd()
                .HasColumnName("con_pNo");
            entity.Property(e => e.textEn)
         .IsUnicode(true)
         .HasColumnName("textEn");
            entity.Property(e => e.titleAr)
         .IsUnicode(true)
         .HasColumnName("titleAr"); entity.Property(e => e.titleAr)
         .IsUnicode(true)
         .HasColumnName("titleAr");
            entity.Property(e => e.textAr)
        .IsUnicode(true)
        .HasColumnName("textAr");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.HasOne(d => d.Emp).WithMany(p => p.Contracting_Policy)
                .HasForeignKey(d => d.EmpId);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback");

            entity.Property(e => e.FBId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FBId");
            entity.Property(e => e.email)
         .IsUnicode(true)
         .HasColumnName("email");
            entity.Property(e => e.text)
         .IsUnicode(true)
         .HasColumnName("text");

            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.HasOne(d => d.User).WithMany(p => p.Feedback)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Policy_Refund>(entity =>
        {
            entity.ToTable("Policy_Refund");

            entity.Property(e => e.p_refN)
                .ValueGeneratedOnAdd()
                .HasColumnName("p_refN");
            entity.Property(e => e.textEn)
         .IsUnicode(true)
         .HasColumnName("textEn");
            entity.Property(e => e.titleAr)
         .IsUnicode(true)
         .HasColumnName("titleAr"); entity.Property(e => e.titleAr)
         .IsUnicode(true)
         .HasColumnName("titleAr");
            entity.Property(e => e.textAr)
        .IsUnicode(true)
        .HasColumnName("textAr");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.HasOne(d => d.Emp).WithMany(p => p.Policy_Refund)
                .HasForeignKey(d => d.EmpId);
        });

        modelBuilder.Entity<Delivery_Period>(entity =>
        {
            entity.ToTable("Delivery_Period");

            entity.Property(e => e.d_pN)
                .ValueGeneratedOnAdd()
                .HasColumnName("d_pN");
            entity.Property(e => e.textEn)
         .IsUnicode(true)
         .HasColumnName("textEn");
            entity.Property(e => e.titleAr)
         .IsUnicode(true)
         .HasColumnName("titleAr"); entity.Property(e => e.titleAr)
         .IsUnicode(true)
         .HasColumnName("titleAr");
            entity.Property(e => e.textAr)
        .IsUnicode(true)
        .HasColumnName("textAr");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.HasOne(d => d.Emp).WithMany(p => p.Delivery_Period)
                .HasForeignKey(d => d.EmpId);
        });
        
        modelBuilder.Entity<HowTobuy>(entity =>
             {
                 entity.ToTable("HowTobuy");
                 entity.Property(e => e.HowBuyN)
                     .ValueGeneratedOnAdd()
                     .HasColumnName("HowBuyN");
                 entity.Property(e => e.textEn)
              .IsUnicode(true)
              .HasColumnName("textEn");
                 entity.Property(e => e.titleAr)
              .IsUnicode(true)
              .HasColumnName("titleAr"); entity.Property(e => e.titleAr)
              .IsUnicode(true)
              .HasColumnName("titleAr");
                 entity.Property(e => e.textAr)
             .IsUnicode(true)
             .HasColumnName("textAr");
                 entity.Property(e => e.EmpId).HasColumnName("empID");
                 entity.HasOne(d => d.Emp).WithMany(p => p.HowTobuy)
                     .HasForeignKey(d => d.EmpId);
             });

        modelBuilder.Entity<PrivacyAndPolicy>(entity =>
        {
            entity.HasKey(e => e.PpNo).HasName("PK__PrivacyA__41E4AB434FEF2435");


            entity.ToTable("PrivacyAndPolicy");

            entity.Property(e => e.PpNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("ppNo");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.TextEn)
                .IsUnicode (true)
                .HasColumnName ("TextEn");
            entity.Property (e => e.TextAr)
             .IsUnicode (true)
             .HasColumnName ("TextAr");

            entity.HasOne(d => d.Emp).WithMany(p => p.PrivacyAndPolicies)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__PrivacyAn__empID__4BAC3F29");
        });
       
        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryNo).HasName("PK__Salaries__336377C79269CBC0");

            entity.Property(e => e.SalaryNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("salaryNo");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.Money).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Emp).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__Salaries__empID__45F365D3");
        });

        modelBuilder.Entity<SocialMedia>(entity =>
        {
            entity.HasKey(e => e.SocialMedNo).HasName("PK__SocialMe__37AA6055C1FEBF49");

            entity.Property(e => e.SocialMedNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName("socialMedNo");
            entity.Property(e => e.EmpId).HasColumnName("empID");
            entity.Property(e => e.ImgLocalPath).HasColumnName("ImgLocalPath");
            entity.Property(e => e.ImgUrl).HasColumnName("ImgUrl");
            entity.Property(e => e.Url).HasColumnName("Url");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("name");

            entity.HasOne(d => d.Emp).WithMany(p => p.SocialMedia)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__SocialMed__empID__571DF1D5");
        });

        modelBuilder.Entity<TermsAndCondition> (entity =>
        {
            entity.HasKey (e => e.TcNo).HasName ("PK__TermsAnd__E07867518DB768ED");

            entity.Property (e => e.TcNo)
                .ValueGeneratedOnAdd ()
                .HasColumnName ("tcNo");
            entity.Property (e => e.EmpId).HasColumnName ("empID");
            entity.Property (e => e.TextEn)
        .IsUnicode (true)
        .HasColumnName ("TextEn");
            entity.Property (e => e.TextAr)
             .IsUnicode (true)
             .HasColumnName ("TextAr");
            entity.HasOne (d => d.Emp).WithMany (p => p.TermsAndConditions)
                .HasForeignKey (d => d.EmpId)
                .HasConstraintName ("FK__TermsAndC__empID__5165187F");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("PK__Titles__4D72D6AAEEDF5763");
            entity.Property(e => e.TitleId)
                .ValueGeneratedOnAdd ()
                .HasColumnName("titleID");
            entity.Property(e => e.TitleName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("titleName");
          

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27442F6DEB");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd ()
                .HasColumnName("ID");
            entity.Property (e => e.Email)
                .HasMaxLength (100)
                .IsUnicode (true)
                .HasColumnName ("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("FullName");
            entity.Property (e => e.PassportNumber);
               entity.HasIndex(u => u.PassportNumber).IsUnique();;
               entity.HasIndex(u => u.Email).IsUnique();;
            entity.Property(e => e.Password)
                .HasMaxLength(450)
                .IsUnicode(true)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(true)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Trainers>(entity =>
        {
            entity.HasKey(e => e.trnId);
            entity.Property(e => e.trnId)
               .ValueGeneratedOnAdd()
               .HasColumnName("trnId");

            entity.HasOne(t => t.Employee)
              .WithMany(i => i.Trainers)
            .HasForeignKey(t => t.empId).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.Email)
             .HasMaxLength(100)
             .IsUnicode(true)
             .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("FullName");
            entity.Property(e => e.Phone)
               .HasMaxLength(50)
               .IsUnicode(true)
               .HasColumnName("Phone");
            entity.HasOne(t => t.Item)
            .WithMany(i => i.Trainers)
            .HasForeignKey(t => t.itemId).OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating (modelBuilder);
    }
    public virtual async Task<int> UpdateBidStateAsync ( )
    {
        return await Database.ExecuteSqlRawAsync ("EXEC UpdateBidState");
    }
}
