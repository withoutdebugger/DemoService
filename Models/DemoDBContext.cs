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
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes{ get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=../../../Data/DBDemoSevice.db;");
        }


    }
}