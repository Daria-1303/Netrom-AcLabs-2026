using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class CategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Tricouri Naționale", Description = "Tricouri oficiale ale echipelor naționale, Cupa Mondială 2026" },
                new Category { Id = 2, Name = "Mingi & Echipament", Description = "Mingi oficiale Trionda și accesorii de joc" },
                new Category { Id = 3, Name = "Accesorii Fani", Description = "Fulare și șepci pentru suporteri" },
                new Category { Id = 4, Name = "Colecționabile", Description = "Mascote de pluș și stickere Panini" }
            );
        }
    }
}
