using Microsoft.EntityFrameworkCore;

namespace ECommerceCoreDapper.Models
{
    public class ECommerceDBContext : DbContext
    {
        public ECommerceDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Products>? Products { get; set; }
        public DbSet<Categories>? Categories { get; set; }
        public DbSet<Address>? Address { get; set; }
        public DbSet<Brands>? Brands { get; set; }
        public DbSet<CreditCards>? CreditCards { get; set; }
        public DbSet<Customers>? Customers { get; set; }
        public DbSet<Orders>? Order { get; set; }
        public DbSet<Payments>? Payments { get; set; }
        public DbSet<ProductPhotos>? ProductPhotos { get; set; }
        public DbSet<Shippers>? Shippers { get; set; }
        public DbSet<Suppliers>? Suppliers { get; set; }
        public DbSet<Comments>? Comments { get; set; }
        public DbSet<Sizes>? Sizes { get; set; }
        public DbSet<Colors>? Colors { get; set; }
        public DbSet<ProductAllJoin> ProductAllJoin { get; set; }

        public DbSet<ProductsOrders> ProductsOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductAllJoin>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView("vw_ProductAllJoin");
                    });
            modelBuilder
               .Entity<ProductsOrders>(
                   eb =>
                   {
                       eb.HasNoKey();
                       eb.ToView("vw_ProductOrder");
                   });
            modelBuilder
                .Entity<ProductsOrders>(
                    eb =>
               {
                   eb.HasNoKey();
                   eb.ToView("vw_DetailSrchProduct");
               });
        }


    }
}
