using BaiThucHanhRazorPage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BaiThucHanhRazorPage.Data
{
    public class QlbanhangContext : DbContext
    {
        public QlbanhangContext(DbContextOptions<QlbanhangContext> options) : base(options)
        {
         
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QLBanHang;Integrated Security=True");
        }

    }
}
