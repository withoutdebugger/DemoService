using Microsoft.EntityFrameworkCore;
using DemoService.Models;

namespace DemoService.Models
{
    public class DemoDBContext : DbContext
    {
        public DemoDBContext(DbContextOptions<DemoDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomersTypes> CustomersTypes{ get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=BDemoSevice.db;");
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_Customers");

                entity.Property(e => e.EmailAddress).HasColumnType("varchar(150)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(50)");

                entity.Property(e => e.MiddleName).HasColumnType("varchar(50)");

                entity.Property(e => e.Notes).HasColumnType("varchar(5000)");

                entity.Property(e => e.SurName).HasColumnType("varchar(50)");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .HasConstraintName("FK_Customers_CustomersTypes");
            });

            modelBuilder.Entity<CustomersTypes>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .HasName("PK_CustomersTypes");

                entity.Property(e => e.CustomerTypeId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("varchar(50)");
            });
        }


    }
}