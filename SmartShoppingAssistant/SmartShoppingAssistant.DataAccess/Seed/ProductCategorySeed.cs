using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class ProductCategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var links = new List<ProductCategory>();

            // Tricouri Naționale (categoria 1): produsele 1–48
            for (int id = 1; id <= 48; id++)
                links.Add(new ProductCategory { ProductId = id, CategoryId = 1 });

            // Mingi & Echipament (categoria 2): produsele 49–55
            for (int id = 49; id <= 55; id++)
                links.Add(new ProductCategory { ProductId = id, CategoryId = 2 });

            // Accesorii Fani (categoria 3): produsele 56–67
            for (int id = 56; id <= 67; id++)
                links.Add(new ProductCategory { ProductId = id, CategoryId = 3 });

            // Colecționabile (categoria 4): produsele 68–74
            for (int id = 68; id <= 74; id++)
                links.Add(new ProductCategory { ProductId = id, CategoryId = 4 });

            modelBuilder.Entity<ProductCategory>().HasData(links);
        }
    }
}
