using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Configurations;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Seed;
namespace SmartShoppingAssistant.DataAccess
{
    public class SmartShoppingAssistantDbContext(DbContextOptions<SmartShoppingAssistantDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Promotion> Promotions { get; set; } = null!;

        public DbSet<CartItem> CartItems { get; set; } = null!;

        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // or facem asta comentata pt fiecare configuratie or facem chestia aia cu assembly si ne scuteste de munca de a adauga fiecare configuratie in parte

            // modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartShoppingAssistantDbContext).Assembly);

            CategorySeed.Seed(modelBuilder);
            ProductSeed.Seed(modelBuilder);
            ProductCategorySeed.Seed(modelBuilder);
            PromotionSeed.Seed(modelBuilder);
        }
    }
}
