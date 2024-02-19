using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class StoreManagementDB : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Client> Clients { get; set; }

        public StoreManagementDB(DbContextOptions<StoreManagementDB> options) : base(options) { }
    }
}
