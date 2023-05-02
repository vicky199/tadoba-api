using Microsoft.EntityFrameworkCore;

namespace tadoba_api.Entity
{
    public class TadobaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TadobaDbContext(DbContextOptions<TadobaDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<DropDownMaster> DropDownMasters { get; set; }
        public virtual DbSet<AppConfig> AppConfigs { get; set; }

    }
}
