using Microsoft.EntityFrameworkCore;

namespace Farma.Entities
{
    public class FarmaContext : DbContext
    {
        public FarmaContext(DbContextOptions options) : base(options) { }

        public DbSet<UsersEntity> Users { get; set; }

        public DbSet<ProductEntity> Product { get; set; }

        public DbSet<CartItemEntity> CartItem { get; set; }

        public DbSet<OrdersEntity> Orders { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public DbSet<ClassEntity> Class { get; set; }

        public DbSet<OriginEntity> Origin { get; set; }

        public DbSet<ProductTypeEntity> ProductType { get; set; }

    }
}
