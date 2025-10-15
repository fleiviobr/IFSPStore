using IFSPStore.Domain.Entities;
using IFSPStore.Repository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace IFSPStore.Repository.Context
{
    public class IFSPStoreContext : DbContext
    {
        public IFSPStoreContext() : base()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=ifspstore;user=root;password=");
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Costumer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SalesItems { get; set; }
        public DbSet<Banana> Banana { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(new ProductMap().Configure);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Category>(new CategoryMap().Configure);
            modelBuilder.Entity<Sale>(new SaleMap().Configure);
            modelBuilder.Entity<SaleItem>(new SaleItemMap().Configure);
            modelBuilder.Entity<City>(new CityMap().Configure);
            modelBuilder.Entity<Costumer>(new CostumerMap().Configure);
            modelBuilder.Entity<Banana>(new BananaMap().Configure);
        }
    }
}
