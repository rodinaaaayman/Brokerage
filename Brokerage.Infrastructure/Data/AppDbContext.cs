using Brokerage.Models;
using Microsoft.EntityFrameworkCore;
//using System.Reflection.Emit;
namespace Brokerage.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Brokers> Brokers { get; set; }
        public DbSet<Executions> Executions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Invoice)
                .WithOne(i => i.Order)
                .HasForeignKey<Invoice>(i => i.OrderId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.OrderId)
                .IsUnique();

            modelBuilder.Entity<Clients>()
                .HasIndex(c => c.Email)
                .IsUnique();
            modelBuilder.Entity<Clients>()
                .HasIndex(c => c.NationalID)
                .IsUnique();
            modelBuilder.Entity<Orders>()
                .Property(o => o.NetAmount)
                .HasComputedColumnSql("[Quantity] * [UnitPrice]", true);


            modelBuilder.Entity<Orders>()
                .Property(o => o.Commission)
                .HasComputedColumnSql("([Quantity] * [UnitPrice]) * [CommissionRate] / 100", true);


            modelBuilder.Entity<Orders>()
                .Property(o => o.GrossAmount)
                .HasComputedColumnSql("([Quantity] * [UnitPrice]) + (([Quantity] * [UnitPrice]) * [CommissionRate] / 100)", true);

            modelBuilder.Entity<Users>()
                .ToTable("Users");

            modelBuilder.Entity<Clients>()
                .ToTable("Clients");

            modelBuilder.Entity<Brokers>()
                .ToTable("Brokers");

            modelBuilder.Entity<Admins>()
                .ToTable("Admins");


            modelBuilder.Entity<Clients>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Clients>()
                .HasIndex(c => c.NationalID)
                .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}
