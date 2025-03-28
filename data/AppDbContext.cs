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
                entity.Property(s => s.SupplierDescription).IsRequired().HasMaxLength(45);

                entity.HasOne(s => s.Company)
                      .WithMany()
                      .HasForeignKey(s => s.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Category)
                      .WithMany()
                      .HasForeignKey(s => s.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}