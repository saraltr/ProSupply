using Microsoft.EntityFrameworkCore;
using CSE_325_group_project.Models;
namespace CSE_325_group_project.Data
{

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<NewOrder> NewOrder { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");
                entity.HasKey(c => c.Company_id);
                entity.Property(c => c.Company_name).IsRequired().HasMaxLength(70);
                entity.Property(c => c.Company_phone).IsRequired().HasMaxLength(45);
                entity.Property(c => c.Company_email).IsRequired().HasMaxLength(45);
                entity.Property(c => c.Company_address).IsRequired();
                entity.Property(c => c.Company_description).HasMaxLength(100);
                entity.Property(c => c.Industry_id).IsRequired();
                entity.Property(c => c.User_id).IsRequired();
            });


            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");
                entity.HasKey(s => s.SupplierId);

                entity.Property(s => s.SupplierName).IsRequired().HasMaxLength(45);
                entity.Property(s => s.SupplierPhone).IsRequired().HasMaxLength(45);
                entity.Property(s => s.SupplierEmail).IsRequired().HasMaxLength(45);
                entity.Property(s => s.SupplierAddress).IsRequired().HasMaxLength(45);
                entity.Property(s => s.SupplierDescription).IsRequired().HasMaxLength(200);

                entity.HasOne(s => s.Company)
                      .WithMany()
                      .HasForeignKey(s => s.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Category)
                      .WithMany()
                      .HasForeignKey(s => s.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<NewOrder>(entity =>
            {
                entity.ToTable("order");
                entity.HasKey(o => o.OrderId);

                entity.Property(o => o.OrderDate).IsRequired();

                entity.Property(o => o.OrderStatus).HasConversion<int>();

                entity.Property(o => o.OrderAmount).IsRequired();
                entity.Property(o => o.OrderAddress1).IsRequired().HasMaxLength(255);
                entity.Property(o => o.OrderAddress2).HasMaxLength(255);
                entity.Property(o => o.OrderCity)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(o => o.OrderZip)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(o => o.OrderCountry)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(o => o.OrderNotes).HasMaxLength(250);
                entity.Property(o => o.SupplierId).IsRequired();
                entity.Property(o => o.UserId).IsRequired();
                entity.HasOne(o => o.Supplier)
                      .WithMany()
                      .HasForeignKey(o => o.SupplierId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.User)
                      .WithMany()
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(u => u.User_id);

                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.UserLastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.UserEmail).IsRequired().HasMaxLength(100);
                entity.Property(u => u.UserPassword).IsRequired();
                entity.Property(u => u.UserLevel).IsRequired();
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("service");
                entity.HasKey(s => s.ServiceId);

                entity.Property(s => s.ServiceName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.ServiceDescription).HasMaxLength(255);
                entity.Property(s => s.ServicePrice).IsRequired();

                entity.HasOne(s => s.Supplier)
                      .WithMany(s => s.Services)
                      .HasForeignKey(s => s.SupplierId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Quote>( entity => 
            {
                entity.ToTable("quote");
                entity.HasKey(q => q.QuoteId);

                entity.Property(q => q.QuoteDate).IsRequired();
                entity.Property(q => q.QuoteDetails).IsRequired().HasMaxLength(300);
                entity.Property(q => q.QuotePrice).HasColumnType("decimal(10,2)");
                entity.Property(q => q.QuoteStatus).HasConversion<int>();
                entity.Property(q => q.QuoteNotes).HasMaxLength(250);

                entity.HasOne(q => q.User)
                .WithMany()
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(q => q.Supplier)
                .WithMany()
                .HasForeignKey(q => q.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            });
        }
    }
}